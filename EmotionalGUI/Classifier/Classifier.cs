using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Accord;

namespace Classifier
{
    public class Classifier
    {
        class SongData
        {
            string filename;
            List<Double> features;

            public SongData()
            {
                features = new List<Double>();
            }
        }

        protected string BEXTRACT_DIRECTORY;
        protected string BEXTRACT_FILENAME;
        protected string FFMPEG_FILENAME;
        protected string TEMP_DIRECTORY;
        protected string WINDOW_FS;
        protected int NUM_BEXTRACT_COLUMNS = 62;

        public Classifier()
        {
            BEXTRACT_DIRECTORY = Directory.GetCurrentDirectory() + "../bin";
            BEXTRACT_FILENAME = BEXTRACT_DIRECTORY + "bextract.exe";
            FFMPEG_FILENAME = BEXTRACT_DIRECTORY + "ffmpeg.exe";
            TEMP_DIRECTORY = Directory.GetCurrentDirectory() + "../tmp";

            WINDOW_FS = (2 ^ 21).ToString();
        }

        public void classifySongs(string[] in_songs, ref double[] out_pos, ref double[] out_energy)
        {

            //Create temporary directory
            if (!Directory.Exists(TEMP_DIRECTORY))
            {
                Directory.CreateDirectory(TEMP_DIRECTORY);
            }

            //Do FFMPEG conversion
            string[] tempfiles = ffmpegConversion(in_songs);

            //Create mkcollection
            string mkcollection = TEMP_DIRECTORY + Path.PathSeparator + "music.mk";
            File.WriteAllLines(mkcollection, tempfiles);

            //Run bextract
            runBextract(mkcollection);

            //Parse bextract data
            parseArff(TEMP_DIRECTORY + Path.PathSeparator + "MARSYAS_EMPTY");
            
            
            return;
        }

        protected string[] ffmpegConversion(string[] files)
        {
            string[] tempfiles = new string[files.Length];
            int i = 0;
            foreach (string file in files)
            {
                //First create temporary file
                string newfile = TEMP_DIRECTORY + Path.GetFileNameWithoutExtension(file);
                while (files.Contains(newfile + ".wav"))
                {
                    newfile += "_dup";
                }
                newfile += ".wav";
                tempfiles[i] = newfile;

                //Run FFMPEG
                string ffmpegArgs = FFMPEG_FILENAME + " -loglevel quiet -y -i" + file + newfile;

                System.Diagnostics.Process ffmpeg = new System.Diagnostics.Process();
                ffmpeg.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                ffmpeg.StartInfo.UseShellExecute = false;
                ffmpeg.StartInfo.RedirectStandardOutput = true;   // Redirect so we can read the standard output
                ffmpeg.StartInfo.Arguments = ffmpegArgs;

                ffmpeg.Start();
                ffmpeg.WaitForExit();
            }
            return tempfiles;
        }

        protected void runBextract(string mkcollection)
        {
            string bextractArgs = BEXTRACT_FILENAME + "-fe -n -ws" + WINDOW_FS + "-od" + TEMP_DIRECTORY + Path.PathSeparator + mkcollection;
            System.Diagnostics.Process bextract = new System.Diagnostics.Process();
            bextract.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            bextract.StartInfo.UseShellExecute = false;
            bextract.StartInfo.RedirectStandardOutput = false;
            bextract.StartInfo.Arguments = bextractArgs;

            bextract.Start();
            bextract.WaitForExit();

        }

        protected void parseArff(string arffFile)
        {
            //List of 62 value arrays.
            //One array for each song
            List<SongData> songData = new List<SongData>();







        }

    }
}
