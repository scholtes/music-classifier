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

        public string Classify(string[] songPaths)
        {
            throw new NotImplementedException();
        }

        public void Train(string[] songPaths, double[] posOutputs, double[] energyOutputs)
        {
            //Get bextract values
            double[][] posInputs = { };
            double[][] energyInputs = { };

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
