using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Classifier;

namespace ClassifierTrainer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Music directory
            string directory = @"T:\Documents\music-classifier\clips_45seconds";

            //Expected energy output
            //Input file is comma separated in the following format:
            //  song1,expectedEnergy,expectedPositivity
            //  song2,expectedEnergy,expectedPositivity
            //
            //song1 is expected to be the filename of the song, and the file is expected to be in the same directory as the csv
            //so if song1 is "hello.mp3", a hello.mp3 file should be in the directory that the csv is located
            List<Song> expectedOutputs = new List<Song>();
            string[] energyLines = System.IO.File.ReadAllLines(@"T:\Documents\music-classifier\clips_45seconds\expected_energy_positivity_small.csv");
            foreach (string line in energyLines)
            {
                string[] parts = line.Split(',');
                string songPath = Path.Combine(directory, parts[0]);
                double expectedEnergy = double.Parse(parts[1]);
                double expectedPositivity = double.Parse(parts[2]);

                Classifier.Song song = new Classifier.Song();
                song.positivity = expectedPositivity;
                song.energy = expectedEnergy;
                song.title = songPath;
                expectedOutputs.Add(song);

            }

            //Train
            Classifier.IClassifierType classifier = new Classifier.SupportVectorMachine();
            classifier.Train(expectedOutputs);
        }
    }
}
