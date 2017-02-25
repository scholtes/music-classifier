using System.IO;

namespace Classifier
{
    public static class BExtract
    {
        private static readonly string WINDOW_FS = (1 << 21).ToString();

        /// <summary>
        /// Run feature extract on the given mkcollection.
        /// </summary>
        /// <param name="mkcollection">Path to mkcollection of songs to run feature extraction on</param>
        /// <returns>Path to .arff file that contains the features for the given mkcollection</returns>
        public static string featureExtraction(string mkcollection)
        {

            string arffFilename = "out.arff";

            //Run bextract with following parameters:
            //  -fe : for feature extraction only
            //  -n  : for normalization
            //  -ws : setting window size to 45s
            //  -hp : setting hopsize to match window size
            //  -od : ouput directory to temporary dir
            //  -w  : output arff file
            string bextractArgs = "-fe -n -ws " + WINDOW_FS + " -hp " + WINDOW_FS + " -od " + ExecutableInformation.getTmpPath() + "/" + " -w " + arffFilename + " " + mkcollection;
            System.Diagnostics.Process bextract = new System.Diagnostics.Process();
            bextract.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;  //Hide the window
            bextract.StartInfo.UseShellExecute = false;
            bextract.StartInfo.RedirectStandardOutput = false;
            bextract.StartInfo.Arguments = bextractArgs;
            bextract.StartInfo.FileName = ExecutableInformation.getBExtractPath();

            bextract.Start();
            bextract.WaitForExit();

            return Path.Combine(ExecutableInformation.getTmpPath(), arffFilename);
        }

        /// <summary>
        /// Write a list of file paths to mkcollection file in the temp directory.
        /// </summary>
        /// <param name="filePaths">List of paths to be included in the mkcolection file.</param>
        /// <returns>Path to output mkcollection</returns>
        public static string convertToMkcollection(string[] filePaths)
        {
            string mkcollectionFile = Path.Combine(ExecutableInformation.getTmpPath(), "music.mk");
            File.WriteAllLines(mkcollectionFile, filePaths);
            return mkcollectionFile;
        }
    }
}
