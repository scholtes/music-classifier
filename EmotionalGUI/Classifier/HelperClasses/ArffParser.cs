using System;
using System.Collections.Generic;
using System.IO;

namespace Classifier
{
    public static class ArffParser
    {
        public static List<SongDataDTO> parseArff(string arffFile)
        {
            List<SongDataDTO> result = new List<SongDataDTO>();


            string arff = File.ReadAllText(arffFile);
            string[] tokens = arff.Split(new[] { "% filename " }, StringSplitOptions.None);

            //Go through each of the songs (skip the first token, that has the bextract attribute comments)
            for (int i = 1; i < tokens.Length; i++)
            {
                string token = tokens[i];

                string[] lines = token.Split('\n');
                string filename = lines[0].Trim(new[] { '\n', '\r' });
                List<List<Double>> extractedFeatures = new List<List<Double>>();
                //Line two is the sampling rate, which we do not use

                //Go through each of the lines of the bextract features
                for (int j = 2; j < lines.Length; j++)
                {
                    string feature_line = lines[j];
                    string[] feature_strs = feature_line.Split(',');
                    List<Double> features = new List<Double>();

                    //Convert each feature into a double, add to the double array
                    //Skip the last one, it will be 'music'
                    for (int k = 0; k < feature_strs.Length - 1; k++)
                    {
                        string feature_str = feature_strs[k];
                        Double feature = Double.Parse(feature_str);
                        features.Add(feature);
                    }
                    extractedFeatures.Add(features);
                }
                SongDataDTO dto = new SongDataDTO(filename, extractedFeatures);
                result.Add(dto);
            }
            return result;
        }
    }
}
