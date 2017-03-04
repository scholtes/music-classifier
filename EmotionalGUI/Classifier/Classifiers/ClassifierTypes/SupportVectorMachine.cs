using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using Accord;
using Accord.IO;
using Accord.Math;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;

namespace Classifier
{
    public class SupportVectorMachine : BaseClassifierType, IClassifierType
    {

        private Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel> posSvm;
        private Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel> energySvm;

        private IEnumerable<int> bextractPosCols = Enumerable.Range(7, 19);     //Values are determined from MATLAB analysis
        private IEnumerable<int> bextractEnergyCols = Enumerable.Range(0, 23);

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SupportVectorMachine()
        {
            //Do nothing
        }

        /// <summary>
        /// Constructor that will load the models from given file paths.
        /// </summary>
        /// <param name="positivitySvmPath">Path to positivity model to load.</param>
        /// <param name="energySvmPath">Path to model to load.</param>
        public SupportVectorMachine(string positivitySvmPath, string energySvmPath)
        {
            LoadModels(positivitySvmPath, energySvmPath);
        }

        /// <summary>
        /// Classify the given songs and output a JSON string with the results.
        /// </summary>
        /// <remarks>
        /// Results will be in the following format:
        /// 
        ///     { "ClassifierResults":
        ///         [ { "song":
        ///             { 
        ///                 "title": "path\to\file\1.mp3",
        ///                 "energy": 0.40120655758066681,
        ///                 "positivity": 0.47041366490774172
        ///             }
        ///           },
        ///           { "song":
        ///             { 
        ///                 "title": "path\to\file\2.mp3",
        ///                 "energy": 0.34171391252526506,
        ///                 "positivity": 0.28074189017885803 
        ///             } 
        ///           },
        ///           ...
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="songPaths">Paths to the songs to classify.</param>
        public string Classify(string[] songPaths)
        {
            if (posSvm == null || energySvm == null)
            {
                throw new Exception("SVMs have not yet been trained!");
            }

            //Get features for all the songs
            List<SongDataDTO> songsWithFeatures = getFeatures(songPaths);

            //Classify each song
            List<ClassifierResult> classifierResults = new List<ClassifierResult>();
            for(int i = 0; i < songsWithFeatures.Count; i++)
            {
                SongDataDTO songData = songsWithFeatures[i];

                ClassifierResult result = new ClassifierResult();
                Song song = new Song();
                song.title = songData.getFilename();

                //Convert dto to double arrays (so svm can use them)
                double[] posFeatures = new double[bextractPosCols.Count()];
                double[] energyFeatures = new double[bextractEnergyCols.Count()];
                ConvertSongDataDtoToDoubleArrays(songData, ref posFeatures, ref energyFeatures);

                //Run classification
                song.positivity = posSvm.Score(posFeatures);
                song.energy = energySvm.Score(energyFeatures);

                result.song = song;
                classifierResults.Add(result);
            }

            EmotionSpaceDTOList emotionSpaceDtoList = new EmotionSpaceDTOList();
            emotionSpaceDtoList.ClassifierResults = classifierResults;
            return JsonSerializer.serializeToJson(emotionSpaceDtoList);
        }

        /// <summary>
        /// Single song override for classification. Returns a JSON string of the results.
        /// </summary>
        /// <param name="songPath">Path to song to classify.</param>
        public string Classify(string songPath)
        {
            return Classify(new string[] { songPath } );
        }

        /// <summary>
        /// Train the model.
        /// </summary>
        /// <remarks>
        /// Input is a list of Song objects which are the expected outputs.
        /// This function will pull the bextract values and use the class's bextract subset for training.
        /// </remarks>
        /// <param name="expectedOutputs">List of Song objects to use for training.</param>
        public void Train(List<Song> expectedOutputs)
        {
            //Pull out the expected outputs
            string[] songPaths = new string[expectedOutputs.Count];
            double[] posOutputs = new double[expectedOutputs.Count];
            double[] energyOutputs = new double[expectedOutputs.Count];
            for (int i = 0; i < expectedOutputs.Count; i++)
            {
                Song song = expectedOutputs[i];
                songPaths[i] = song.title;
                posOutputs[i] = song.positivity;
                energyOutputs[i] = song.energy;
            }

            //Get bextract values
            System.Console.WriteLine(System.DateTime.Now.ToString() + " Extracting features...");
            List<SongDataDTO> songFeatures = getFeatures(songPaths);

            //Stick them in double arrays
            double[][] posInputs = new double[songFeatures.Count][];
            double[][] energyInputs = new double[songFeatures.Count][];
            for (int i = 0; i < songFeatures.Count; i++)
            {
                ConvertSongDataDtoToDoubleArrays(songFeatures[i], ref posInputs[i], ref energyInputs[i]);
            }

            //Train
            System.Console.WriteLine(System.DateTime.Now.ToString() + " Training positivity.");
            var learn = new SequentialMinimalOptimizationRegression()
            {
                Kernel = new Gaussian(0.25), 
                UseComplexityHeuristic = true
            }; 
            posSvm = learn.Learn(posInputs, posOutputs);

            System.Console.WriteLine(System.DateTime.Now.ToString() + " Training energy.");
            learn = new SequentialMinimalOptimizationRegression()
            {
                Kernel = new Gaussian(0.5),
                UseComplexityHeuristic = true
            };
            energySvm = learn.Learn(energyInputs, energyOutputs);

        }

        /// <summary>
        /// Load the models from files. These files should be generated by the SaveModels function.
        /// </summary>
        /// <param name="positivityPath">Path to positivity SVM.</param>
        /// <param name="energyPath">Path to energy SVM.</param>
        public override void LoadModels(string positivityPath, string energyPath)
        {
            posSvm = Serializer.Load<Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel>>(positivityPath);
            energySvm = Serializer.Load<Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel>>(energyPath);
        }

        /// <summary>
        /// Save the models to file. Should only be called after training the models.
        /// </summary>
        /// <param name="positivityPath">Path to positivity SVM.</param>
        /// <param name="energyPath">Path to energy SVM.</param>
        public override void SaveModels(string positivityPath, string energyPath)
        {
            Serializer.Save(posSvm, positivityPath);
            Serializer.Save(energySvm, energyPath);
        }

        /// <summary>
        /// Extract the data from the SongDataDTO object and place into arrays of doubles. This is needed for training.
        /// </summary>
        /// <remarks>
        /// Only pulls out the bextract columns for positivity/energy.
        /// This function will allocate the arrays.
        /// </remarks>
        /// <param name="songData">SongDataDTO object containing the bextract data to use for training.</param>
        /// <param name="posFeatures">Reference to double array to store the features needed for training positivity.</param>
        /// <param name="energyFeatures">Reference to double array to store the features needed for training energy.</param>
        private void ConvertSongDataDtoToDoubleArrays(SongDataDTO songData, ref double[] posFeatures, ref double[] energyFeatures)
        {
            //Stick them in double arrays
            List<List<Double>> features = songData.getFeatures();
            List<Double> featureList = features[0]; //Just pull the first row of features for now

            posFeatures = new double[bextractPosCols.Count() + 1];
            energyFeatures = new double[bextractEnergyCols.Count() + 1];

            //Pull out relevant bextract columns
            for(int i = 1; i < bextractPosCols.Count(); i++)
            {
                int col = bextractPosCols.ElementAt(i);
                posFeatures[i] = featureList[col];
            }
            for(int i = 1; i < bextractEnergyCols.Count(); i++)
            {
                int col = bextractEnergyCols.ElementAt(i);
                energyFeatures[i] = featureList[col];
            }
        }
    }
}
