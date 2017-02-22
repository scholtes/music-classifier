using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifier;

namespace ClassifierTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Classifier.SupportVectorMachine svm = new Classifier.SupportVectorMachine();
            svm.Classify(new[] { @"T:\Documents\music-classifier\Classifier\test.mp3" });
        }
    }
}
