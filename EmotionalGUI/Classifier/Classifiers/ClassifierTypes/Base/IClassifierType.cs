using System.Collections.Generic;

namespace Classifier
{
    public interface IClassifierType
    {
        string Classify(string[] songPaths);

        void Train(List<SongDataDTO> songsAndFeatures);
    }
}
