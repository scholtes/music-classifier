using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public class Classifier
    {
        private IClassifierType classifier;
        private List<SongDataDTO> extractedFeatures;

        public Classifier(IClassifierType classifierType)
        {
            classifier = classifierType;
        }

        public void ConvertToWav(string[] filePaths)
        {
            FFMpeg.ffmpegConversion(filePaths);
        }

        public void ExtractFeaturesToFile(string mkcollectionPath)
        {
            BExtract.getFeatures(mkcollectionPath);
        }

        public void LoadFeaturesFromFile(string arffFilePath)
        {
            extractedFeatures = ArffParser.parseArff(arffFilePath);
        }

        public string Classify(string[] songPaths)
        {
            return classifier.Classify(songPaths);
        }

        public void Train()
        {
            classifier.Train(extractedFeatures);
        }
    }
}
