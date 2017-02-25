using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Framework
{
    class Classifier
    {
        public string classify(string songpath)
        {
            string output = null;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"..\..\..\..\Classifier\dist\emotify\emotify.exe";
            startInfo.Arguments += "\"" + songpath + "\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            //startInfo.RedirectStandardError = true;
            var proc = new Process();
            proc.StartInfo = startInfo;
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                output = proc.StandardOutput.ReadLine();
            }

            return output;
        }
    }
}
