using System;
using System.Collections.Generic;
using System.IO;

namespace Framework
{
    public static class DirectoryBrowser
    {
        public static string[] getSongs(string directory)
        {
            if (!Directory.Exists(directory))
            {
                throw new ArgumentException("Not a valid Directory");
            }

            List<string> songs = new List<string>();
            getSongs(directory, ref songs);
            return songs.ToArray();
        }

        private static void getSongs(string directory,ref List<string> songs)
        {
            try
            {
                string[] directories = Directory.GetDirectories(directory);
                string[] files = Directory.GetFiles(directory);

                foreach (string file in files)
                {
                    foreach (string filetype in SupportedFiletypes.Types)
                    {
                        if (file.EndsWith(filetype))
                        {
                            songs.Add(file);
                            break;
                        }
                    }
                }
                foreach (string dir in directories)
                {
                    getSongs(dir, ref songs);
                }
            }
            catch
            {

            }
        }
    }
}
