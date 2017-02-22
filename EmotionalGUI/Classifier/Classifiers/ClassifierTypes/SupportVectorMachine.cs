using System.Collections.Generic;
using System;
using Accord;
using Accord.IO;
using Accord.Math;
using Accord.MachineLearning.VectorMachines;
using Accord.Statistics.Kernels;

namespace Classifier
{
    public class SupportVectorMachine : BaseClassifierType, IClassifierType
    {

        private Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel> posSvm;
        private Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel> energySvm;

        private int[] bextractPosCols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int[] bextractEnergyCols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public string Classify(string[] songPaths)
        {
            throw new NotImplementedException();
        }

        public void Train(List<Song> expectedOutputs)
        {
            //Pull out the expected outputs
            string[] songPaths = new string[expectedOutputs.Count];
            double[] posOutputs = new double[expectedOutputs.Count];
            double[] energyOutputs = new double[expectedOutputs.Count];
            for(int i = 0; i < expectedOutputs.Count; i++) 
            {
                Song song = expectedOutputs[i];
                songPaths[i] = song.title;
                posOutputs[i] = song.positivity;
                energyOutputs[i] = song.energy;
                
            }

            //Get bextract values
            //Features might not be in the same order we gave the song paths
            List<SongDataDTO> songFeatures = getFeatures(songPaths);

            //Stick them in double arrays
            double[][] posInputs = new double[songFeatures.Count][];
            double[][] energyInputs = new double[songFeatures.Count][];
            for (int i = 0; i < songFeatures.Count; i++)
            {
                SongDataDTO song = songFeatures[i];
                List<List<Double>> features = song.getFeatures();

                posInputs[i] = new double[bextractPosCols.Length];
                energyInputs[i] = new double[bextractEnergyCols.Length];

                List<Double> featureList = features[0]; //Just pull the first row of features for now

                foreach (int col in bextractEnergyCols)
                {
                    energyInputs[i][col] = featureList[col];    
                }
                foreach(int col in bextractPosCols)
                {
                    posInputs[i][col] = featureList[col];
                }
            }
            

            //Train
            var learn = new Accord.MachineLearning.VectorMachines.Learning.SequentialMinimalOptimization<IKernel>()
            {
                Kernel = new Gaussian(5) //Not sure what this should be
            };
            posSvm = learn.Learn(posInputs, posOutputs);
            energySvm = learn.Learn(energyInputs, energyOutputs);

        }

        public override void LoadClassifier()
        {
            throw new NotImplementedException();
        }

        public override void SaveClassifier()
        {
            throw new NotImplementedException();
        }
    }
}
