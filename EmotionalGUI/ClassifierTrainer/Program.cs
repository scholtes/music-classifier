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
            string directory = @"E:\Music\Duran Duran\";
            Classifier.IClassifierType classifier = new Classifier.SupportVectorMachine();
            string[] songPaths = Framework.DirectoryBrowser.getSongs(directory);
            classifier.Train(songPaths);
        }
    }
}
