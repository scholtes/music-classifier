using System.Collections.Generic;
using System;

namespace Classifier
{
    public abstract class BaseClassifierType
    {

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
        /// Write a list of file paths to mkcollection file.
        /// </summary>
        /// <param name="filePaths">List of paths to be included in the mkcolection file.</param>
        /// <returns>Path to output mkcollection<s/returns>
        protected string MakeMkcollection(string[] filePaths)
        {
            return BExtract.convertToMkcollection(filePaths);
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
        protected List<SongDataDTO> LoadFeaturesFromFile(string arffFilePath)
        {
            return ArffParser.parseArff(arffFilePath);
        }

        /// <summary>
        /// Runs feature extraction on the given files.
        /// </summary>
        /// <param name="filePaths">Paths of files to run feature extraction on.</param>
        /// <returns>List of SongDataDTOs containing extracted features.</returns>
        protected List<SongDataDTO> getFeatures(string[] filePaths)
        {
            string[] wavPaths = ConvertToWav(filePaths);
            string mkcollectionFile = MakeMkcollection(wavPaths);
            string arffFilePath = ExtractFeaturesToFile(mkcollectionFile);
            return LoadFeaturesFromFile(arffFilePath);
        }

        public abstract void LoadClassifier(string positivityPath, string energyPath);

        public abstract void SaveClassifier(string positivityPath, string energyPath);
    }
}
