using System.IO;

namespace Classifier
{
    public static class ExecutableInformation
    {
        public static string getBExtractPath()
        {
           return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\common_utilities\\bextract.exe"));
        }

        public static string getFFMPegPath()
        {
            return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\..\\common_utilities\\ffmpeg.exe"));
        }

        public static string getTmpPath()
        {
            return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\tmp"));
        }
    }
}
