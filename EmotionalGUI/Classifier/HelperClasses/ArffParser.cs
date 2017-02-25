using System;
using System.Collections.Generic;
using System.IO;

namespace Classifier
{
    public static class ArffParser
    {

        /// <summary>
        /// Parses the .arff file output from bextract.
        /// </summary>
        /// <param name="arffFile">Full path to .arff file to parse.</param>
        /// <returns>List of SongDataDTOs containing extracted features.</returns>
        public static List<SongDataDTO> parseArff(string arffFile)
        {
            List<SongDataDTO> result = new List<SongDataDTO>();
            string arff = File.ReadAllText(arffFile);
            string[] tokens = arff.Split(new[] { "% filename " }, StringSplitOptions.None);

            //Go through each of the songs (skip the first token, that has the bextract attribute comments)
            for (int i = 1; i < tokens.Length; i++)
            {
                string songInfo = tokens[i];
                SongDataDTO dto = parseSongInfo(songInfo);
                result.Add(dto);
            }
            return result;
        }

        /// <summary>
        /// Parses a song's section from the .arff file output of.
        /// </summary>
        /// <param name="songInfo">The song section of the .arff file. Between "% filename " tokens.</param>
        /// <returns>SongDataDTO containing extracted features</returns>
        private static SongDataDTO parseSongInfo(string songInfo)
        {
            string[] lines = songInfo.Split('\n');

            //Line 1 is the filename
            string filename = lines[0].Trim(new[] { '\n', '\r' });

            //Line two is the sampling rate, which we do not use

            //Following lines are extracted features for each sample of the song
            List<List<Double>> extractedFeatures = new List<List<Double>>();
            for (int i = 2; i < lines.Length; i++)
            {
                string featureLine = lines[i];
                List<Double> features = parseFeatureLine(featureLine);
                extractedFeatures.Add(features);
            }
            SongDataDTO dto = new SongDataDTO(filename, extractedFeatures);
            return dto;
        }

        /// <summary>
        /// Parses one line of features from the song's section in the .arff file output of bextract.
        /// </summary>
        /// <param name="featureLine">CSV line of extracted features.</param>
        /// <returns>List of Doubles containing extracted feature values.</returns>
        private static List<Double> parseFeatureLine(string featureLine)
        {
            string[] feature_strs = featureLine.Split(',');
            List<Double> features = new List<Double>();

            //Convert each feature into a double, add to the double array
            //Skip the last one, it will be 'music'
            for (int i = 0; i < feature_strs.Length - 1; i++)
            {
                string feature_str = feature_strs[i];
                Double feature = Double.Parse(feature_str);
                features.Add(feature);
            }
            return features;
        }

    }
}
