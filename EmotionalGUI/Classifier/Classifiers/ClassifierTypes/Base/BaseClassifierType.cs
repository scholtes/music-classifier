using System.Collections.Generic;
using System;

namespace Classifier
{
    public abstract class BaseClassifierType
    {
        private List<SongDataDTO> songsAndFeatures;

        protected string[] ConvertToWav(string[] filePaths)
        {
            return FFMpeg.ffmpegConversion(filePaths);
        }

        protected string ExtractFeaturesToFile(string mkcollectionPath)
        {
            return BExtract.featureExtraction(mkcollectionPath);
        }

        protected void LoadFeaturesFromFile(string arffFilePath)
        {
            songsAndFeatures = ArffParser.parseArff(arffFilePath);
        }

        protected List<SongDataDTO> getFeatures(string[] filePaths)
        {
            string[] wavPaths = ConvertToWav(filePaths);
            string mkcollectionFile = BExtract.convertToMkcollection(wavPaths);
            string arffFilePath = ExtractFeaturesToFile(mkcollectionFile);
            return ArffParser.parseArff(arffFilePath);
        }

        public abstract void LoadClassifier();

        public abstract void SaveClassifier();
    }
}
