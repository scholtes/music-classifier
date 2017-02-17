using System.Collections.Generic;
using System;

namespace Classifier
{
    public class MultivariateLinearRegression : BaseClassifierType,IClassifierType
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
