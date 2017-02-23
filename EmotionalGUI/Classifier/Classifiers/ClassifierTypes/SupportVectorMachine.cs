﻿using System.Collections.Generic;
using System;
using System.IO;
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

        private Accord.MachineLearning.VectorMachines.SupportVectorMachine<Polynomial> posSvm;
        private Accord.MachineLearning.VectorMachines.SupportVectorMachine<Polynomial> energySvm;

        private int[] bextractPosCols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int[] bextractEnergyCols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

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
                double[] posFeatures = new double[bextractPosCols.Length];
                double[] energyFeatures = new double[bextractEnergyCols.Length];
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
            List<SongDataDTO> songFeatures = getFeatures(songPaths);

            //Stick them in double arrays
            double[][] posInputs = new double[songFeatures.Count][];
            double[][] energyInputs = new double[songFeatures.Count][];
            for (int i = 0; i < songFeatures.Count; i++)
            {
                ConvertSongDataDtoToDoubleArrays(songFeatures[i], ref posInputs[i], ref energyInputs[i]);
            }

            //Train
            //var learn = new SequentialMinimalOptimizationRegression<Gaussian>()
            //{
            //    Complexity = 100
            //};
            var learn = new SequentialMinimalOptimizationRegression<Polynomial>()
            {
                Kernel = new Polynomial(2),
                Complexity = 100
            };
            posSvm = learn.Learn(posInputs, posOutputs);
            energySvm = learn.Learn(energyInputs, energyOutputs);

            //Save classifier
            SaveClassifier(Path.Combine(ExecutableInformation.getTmpPath(), "positivity.svm"), Path.Combine(ExecutableInformation.getTmpPath(), "energy.svm"));

        }

        public override void LoadClassifier(string positivityPath, string energyPath)
        {
            posSvm = Serializer.Load<Accord.MachineLearning.VectorMachines.SupportVectorMachine<Polynomial>>(positivityPath);
            energySvm = Serializer.Load<Accord.MachineLearning.VectorMachines.SupportVectorMachine<Polynomial>>(energyPath);
        }

        public override void SaveClassifier(string positivityPath, string energyPath)
        {
            Serializer.Save(posSvm, positivityPath);
            Serializer.Save(energySvm, energyPath);
        }

        private void ConvertSongDataDtoToDoubleArrays(SongDataDTO songData, ref double[] posFeatures, ref double[] energyFeatures)
        {
            //Stick them in double arrays
            List<List<Double>> features = songData.getFeatures();
            List<Double> featureList = features[0]; //Just pull the first row of features for now

            posFeatures = new double[bextractPosCols.Length];
            energyFeatures = new double[bextractEnergyCols.Length];

            foreach (int col in bextractEnergyCols)
            {
                posFeatures[col] = featureList[col];
            }
            foreach (int col in bextractPosCols)
            {
                energyFeatures[col] = featureList[col];
            }
        }
    }
}
