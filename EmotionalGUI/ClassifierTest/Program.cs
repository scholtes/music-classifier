using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Classifier;

namespace ClassifierTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Classifier.SupportVectorMachine svm = new Classifier.SupportVectorMachine();
            svm.LoadClassifier(Path.Combine(@"T:\Documents\music-classifier\Classifier", "positivity.svm"),
                               Path.Combine(@"T:\Documents\music-classifier\Classifier", "energy.svm"));

            string jsonOutput = svm.Classify(new[] { @"T:\Documents\music-classifier\Classifier\2.mp3" });
            System.Console.WriteLine(jsonOutput);
            System.Console.ReadKey();
        }
    }
}
