using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassifierTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Music directory
            string directory = @"T:\Documents\music-classifier\clips_45seconds";
            string[] songPaths = Framework.DirectoryBrowser.getSongs(directory);

            //Expected energy output
            IEnumerable<Double> energyOutput = System.IO.File.ReadAllLines(@"T:\Documents\music-classifier\clips_45seconds\wavs\expected_energy.csv").Select(l => Double.Parse(l));
            double[] expectedEnergyOutput = energyOutput.ToArray<double>();

            IEnumerable<Double> positivityOutput = System.IO.File.ReadAllLines(@"T:\Documents\music-classifier\clips_45seconds\wavs\expected_positivity.csv").Select(l => Double.Parse(l));
            double[] expectedpositivityOutput = energyOutput.ToArray<double>();

            //Train
            Classifier.IClassifierType classifier = new Classifier.SupportVectorMachine();
            classifier.Train(songPaths, expectedpositivityOutput, expectedEnergyOutput);
        }
    }
}
