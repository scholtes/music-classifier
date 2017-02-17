using System.Collections.Generic;
using System;

namespace Classifier
{
    public class SupportVectorMachine : BaseClassifierType,IClassifierType
    {
        public string Classify(string[] songPaths)
        {
            throw new NotImplementedException();
        }

        public void Train(List<SongDataDTO> songsAndFeatures)
        {
            throw new NotImplementedException();
        }
    }
}
