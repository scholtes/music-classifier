using System.Collections.Generic;
using System;

namespace Classifier
{
    public abstract class BaseClassifierType
    {
        private List<SongDataDTO> songsAndFeatures;

        /// <summary>
        /// Convert a list of songs to .wav format.
        /// </summary>
        /// <param name="filePaths">List of paths to be converted to .wav format.</param>
        /// <returns>List of paths of converted songs in .wav format</returns>
        protected string[] ConvertToWav(string[] filePaths)
        {
            return FFMpeg.ffmpegConversion(filePaths);
        }

        /// <summary>
        /// Run feature extract on the given mkcollection.
        /// </summary>
        /// <param name="mkcollectionPath">Path to mkcollection of songs to run feature extraction on</param>
        /// <returns>Path to .arff file that contains the features for the given mkcollection</returns>
        protected string ExtractFeaturesToFile(string mkcollectionPath)
        {
            return BExtract.featureExtraction(mkcollectionPath);
        }

        /// <summary>
        /// Extract features from given .arff file and store into private list of SongDataDTOs
        /// </summary>
        /// <param name="arffFilePath">Path to .arff containing extracted features.</param>
        protected void LoadFeaturesFromFile(string arffFilePath)
        {
            songsAndFeatures = ArffParser.parseArff(arffFilePath);
        }

        /// <summary>
        /// Runs feature extraction on the given files.
        /// </summary>
        /// <param name="filePaths">Paths of files to run feature extraction on.</param>
        /// <returns>List of SongDataDTOs containing extracted features.</returns>
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
