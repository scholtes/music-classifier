using System.Collections.Generic;

namespace Classifier
{
    public interface IClassifierType
    {
        string Classify(string[] songPaths);

        string Classify(string songPath);

        void Train(List<Song> expectedOutputs);
    }
}
