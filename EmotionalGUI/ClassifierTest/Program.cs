using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Classifier;
using Framework;

namespace ClassifierTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create and load classifier
            Classifier.SupportVectorMachine svm = new Classifier.SupportVectorMachine();
            svm.LoadModels(Path.Combine(@"T:\Documents\music-classifier\EmotionalGUI\resources\models", "positivity_gaussian.svm"),
                               Path.Combine(@"T:\Documents\music-classifier\EmotionalGUI\resources\models", "energy_gaussian.svm"));

            string directory = @"T:\Documents\music-classifier\clips_45seconds";
            
            List<Classifier.Song> expectedOutputs = new List<Classifier.Song>();
            string[] songLines = System.IO.File.ReadAllLines(@"T:\Documents\music-classifier\clips_45seconds\expected_energy_positivity_random_test.csv");

            double energy_total = 0;
            double energy_mean = 0;
            double energy_SStot = 0;
            double energy_SSreg = 0;
            double energy_SSres = 0;
            double energy_r_squared = 0;
            double energy_accuracy = 0;
            double energy_rmse = 0;
            int energy_correct = 0;
            List<double> energy_predicted = new List<double>();
            List<double> energy_stds = new List<double>();
            List<double> energy_expected = new List<double>();

            double positivity_total = 0;
            double positivity_mean = 0;
            double positivity_SStot = 0;
            double positivity_SSreg = 0;
            double positivity_SSres = 0;
            double positivity_r_squared = 0;
            double positivity_accuracy = 0;
            double positivity_rmse = 0;
            int positivity_correct = 0;
            List<double> positivity_predicted = new List<double>();
            List<double> positivity_stds = new List<double>();
            List<double> positivity_expected = new List<double>();

            List<string> songPaths = new List<string>();
            foreach (string line in songLines)
            {
                //Get expected values
                string[] parts = line.Split(',');
                string songPath = Path.Combine(directory, parts[0]);
                double expectedEnergy = double.Parse(parts[1]);
                double expectedPositivity = double.Parse(parts[2]);
                double energyStd = double.Parse(parts[3]);
                double positivityStd = double.Parse(parts[4]);

                songPaths.Add(songPath);
                energy_expected.Add(expectedEnergy);
                energy_stds.Add(energyStd);

                positivity_expected.Add(expectedPositivity);
                positivity_stds.Add(positivityStd);
                
            }

            //Classify
            string jsonOutput = svm.Classify(songPaths.ToArray());

            //Retrieve the results
            JsonDTO jsonDTO = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<JsonDTO>(jsonOutput);
            List<Framework.ClassifierResult> classifierResults = jsonDTO.ClassifierResults;

            //Get stastics
            //Assuming output is in same order as input (it should be)
            foreach(Framework.ClassifierResult result in classifierResults)
            {
                double predictedEnergy = result.song.energy;
                double predictedPositivity = result.song.positivity;

                energy_total += predictedEnergy;
                positivity_total += predictedPositivity;

                energy_predicted.Add(predictedEnergy);
                positivity_predicted.Add(predictedPositivity);
            }

            energy_mean = energy_total / energy_predicted.Count();
            positivity_mean = positivity_total / positivity_predicted.Count();

            for (int i = 0; i < positivity_predicted.Count(); i++)
            {
                double predictedEnergy = energy_predicted[i];
                double expectedEnergy = energy_expected[i];
                double energyStd = energy_stds[i];

                double predictedPositivity = positivity_predicted[i];
                double expectedPositivity = positivity_expected[i];
                double positivityStd = positivity_stds[i];
                
                energy_correct += Math.Abs(predictedEnergy - expectedEnergy) < energyStd ? 1 : 0;
                energy_SSres += Math.Pow(predictedEnergy - expectedEnergy, 2);
                energy_SSreg += Math.Pow(predictedEnergy - energy_mean, 2);
                energy_SStot += Math.Pow(expectedEnergy - energy_mean, 2);

                positivity_correct += Math.Abs(predictedPositivity - expectedPositivity) < positivityStd ? 1 : 0;
                positivity_SSres += Math.Pow(predictedPositivity - expectedPositivity, 2);
                positivity_SSreg += Math.Pow(predictedPositivity - positivity_mean, 2);
                positivity_SStot += Math.Pow(expectedPositivity - positivity_mean, 2);
            }
            energy_accuracy = (double)energy_correct / energy_predicted.Count();
            energy_r_squared = 1 - energy_SSreg / energy_SStot;
            energy_rmse = Math.Pow(energy_SStot / energy_predicted.Count(), 0.5);

            positivity_accuracy = (double)positivity_correct / positivity_predicted.Count();
            positivity_r_squared = 1 - positivity_SSreg / positivity_SStot;
            positivity_rmse = Math.Pow(positivity_SStot / positivity_predicted.Count(), 0.5);

            //Print
            System.Console.WriteLine("Accuracy (energy)\t=\t" + energy_accuracy*100 + "%");
            System.Console.WriteLine("Accuracy (positivity)\t=\t" + positivity_accuracy*100 + "%");
            System.Console.WriteLine("Accuracy (total)\t=\t" + energy_accuracy * positivity_accuracy * 100 + "%");
            System.Console.WriteLine();
            System.Console.WriteLine("RMSE (energy)\t\t=\t" + energy_rmse);
            System.Console.WriteLine("RMSE (positivity)\t=\t" + positivity_rmse);
            System.Console.WriteLine();
            System.Console.WriteLine("R^2 (energy)\t\t=\t" + energy_r_squared);
            System.Console.WriteLine("R^2 (positivity)\t=\t" + positivity_r_squared);
           
            System.Console.ReadKey();
        }
    }
}
