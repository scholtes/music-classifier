using System;
using System.IO;
using System.Collections.Generic;

namespace Framework
{
    public class PlayList
    {
        private string dir = null;
        private LinkedList<string> playlist = null;

        private PlayList() { }
        public PlayList(string directory)
        {
            dir = directory;
            populatePlaylist();
        }

        // Creates a playlist supported filetypes given a directory (non recursive)
        private void populatePlaylist()
        {
            playlist = new LinkedList<string>();
            string[] files = Directory.GetFiles(dir);
            foreach (string file in files)
            {
                foreach (string filetype in SupportedFiletypes.Types)
                {
                    if (file.Contains(filetype))
                    {
                        playlist.AddLast(file);
                        break;
                    }
                }
            }
        }

        public string getCurrentSong()
        {
            return playlist.First.Value;
        }

        public string getNextSong()
        {
            string song = playlist.First.Value;
            playlist.RemoveFirst();
            playlist.AddLast(song);
            return playlist.First.Value;
        }

        public string getPrevSong()
        {
            string song = playlist.Last.Value;
            playlist.RemoveLast();
            playlist.AddFirst(song);
            return song;
        }
    }
}
