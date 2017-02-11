using System;
using System.Runtime.InteropServices;

namespace Framework
{
    public class Classifier : IClassifier
    {
        public string classifySongs(string[] songs)
        {

            //Format the songs into single comma-separated string
            string emotifyArgs = formatSongs(songs);

            System.Diagnostics.Process emotify = new System.Diagnostics.Process();
            emotify.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            emotify.StartInfo.UseShellExecute = false;
            emotify.StartInfo.RedirectStandardOutput = true;   // Redirect so we can read the standard output

            //Call "emotify.exe path/to/song1.mp3 path/to/song2.mp3 ... "
            emotify.StartInfo.FileName = @"..\..\..\..\Classifier\dist\emotify\emotify.exe";
            emotify.StartInfo.Arguments = emotifyArgs;

            //Run the process
            emotify.Start();
            string output = emotify.StandardOutput.ReadToEnd();

            return output;
        }


        //Add quotes around each song in the given string array
        //Join songs into one string by adding space delimiter
        private string formatSongs(string[] songs)
        {
            string formattedSongs = "";
            foreach (string song in songs)
            {

                formattedSongs += "\"" + song + "\" ";
            }

            return formattedSongs;
        }
    }
}
