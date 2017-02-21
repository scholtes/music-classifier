using System.IO;

namespace Classifier
{
    public static class ExecutableInformation
    {
        public static string getBExtractPath()
        {
           return Path.GetFullPath(Directory.GetCurrentDirectory() + "..\\bin\\bextract.exe");
        }

        public static string getFFMPegPath()
        {
            return Path.GetFullPath(Directory.GetCurrentDirectory() + "..\\bin\\ffmpeg.exe");
        }

        public static string getTmpPath()
        {
            return Path.GetFullPath(Directory.GetCurrentDirectory() + "..\\tmp");
        }
    }
}
