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

        Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel> posSvm;
        Accord.MachineLearning.VectorMachines.SupportVectorMachine<IKernel> energySvm;

        int[] bextractPosCols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int[] bextractEnergyCols = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        public string Classify(string[] songPaths)
        {
            throw new NotImplementedException();
        }

        public void Train(string[] songPaths, double[] posOutputs, double[] energyOutputs)
        {
            //Get bextract values
            List<SongDataDTO> songFeatures = getFeatures(songPaths);

            //Stick them in double arrays
            double[][] posInputs = { };
            double[][] energyInputs = { };
            for (int i = 0; i < songFeatures.Count; i++)
            {
                SongDataDTO song = songFeatures[i];
                List < List < Double >> features = song.getFeatures();
                foreach(int col in bextractEnergyCols)
                {
                    energyInputs[i][col] = features[0][col];    //Just pull the first row of features for now
                }
                foreach(int col in bextractPosCols)
                {
                    posInputs[i][col] = features[0][col];
                }
            }
            

            //Train
            var learn = new Accord.MachineLearning.VectorMachines.Learning.SequentialMinimalOptimization<IKernel>()
            {
                Kernel = new Gaussian(5) //Not sure what this should be
            };
            posSvm = learn.Learn(posInputs, posOutputs);
            energySvm = learn.Learn(energyInputs, energyOutputs);

            throw new NotImplementedException();
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
