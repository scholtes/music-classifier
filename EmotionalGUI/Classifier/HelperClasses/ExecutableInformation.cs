using System.IO;

namespace Classifier
{
    public static class ExecutableInformation
    {
        public static string getBExtractPath()
        {
           return Directory.GetCurrentDirectory() + "..\\bin\\bextract.exe";
        }

        public static string getFFMPegPath()
        {
            return Directory.GetCurrentDirectory() + "..\\bin\\ffmpeg.exe";
        }

        public static string getTmpPath()
        {
            return Directory.GetCurrentDirectory() + "..\\tmp";
        }
    }
}
