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

        //Returns the temporary working directory. Will create if it doesn't exist
        public static string getTmpPath()
        {
            string dir = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..\\tmp"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return dir;
        }
    }
}
