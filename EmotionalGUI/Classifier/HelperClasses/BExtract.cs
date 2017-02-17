
namespace Classifier
{
    public static class BExtract
    {
        private static readonly string WINDOW_FS = (1 << 21).ToString();

        public static void getFeatures(string mkcollection)
        {
            string bextractArgs = "-fe -n -ws " + WINDOW_FS + " -hp " + WINDOW_FS + " -od " + ExecutableInformation.getTmpPath() + "/" + " " + mkcollection;
            System.Diagnostics.Process bextract = new System.Diagnostics.Process();
            bextract.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            bextract.StartInfo.UseShellExecute = false;
            bextract.StartInfo.RedirectStandardOutput = false;
            bextract.StartInfo.Arguments = bextractArgs;
            bextract.StartInfo.FileName = ExecutableInformation.getBExtractPath();

            bextract.Start();
            bextract.WaitForExit();

        }
    }
}
