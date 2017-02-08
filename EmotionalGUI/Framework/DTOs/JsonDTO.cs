using System.Collections.Generic;
using System;

namespace Framework
{
    [Serializable]
    public class JsonDTO
    {
        public List<ClassifierResult> ClassifierResults { get; set; }
    }

    [Serializable]
    public class Song
    {
        public string title { get; set; }
        public double energy { get; set; }
        public double positivity { get; set; }
    }

    [Serializable]
    public class ClassifierResult
    {
        public Song song { get; set; }
    }
}
