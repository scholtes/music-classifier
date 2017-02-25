using System;
using System.Collections.Generic;

namespace Classifier
{
    /// <summary>
    /// Immutable class to contain one song and all extracted features of that song
    /// </summary>
    public class SongDataDTO
    {
        private string _filename;
        private List<List<Double>> _features;

        public SongDataDTO(string filename, List<List<Double>> features)
        {
            this._filename = filename;
            this._features = features;
        }

        public string getFilename()
        {
            return _filename;
        }

        public List<List<Double>> getFeatures()
        {
            return _features;
        }
    }
}
