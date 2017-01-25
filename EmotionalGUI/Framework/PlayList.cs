using System;
using System.IO;
using System.Collections.Generic;

namespace Framework
{
    public class PlayList
    {
        #region Properties
        private string dir = null;
        private LinkedList<string> playlist = null;
        #endregion

        #region Constructors
        private PlayList() { }
        public PlayList(string directory)
        {
            if(String.IsNullOrEmpty(directory))
            {
                throw new ArgumentException("Directory cannot be empty");
            }
            if (!Directory.Exists(directory))
            {
                throw new ArgumentException("Invalid Directory");
            }
            dir = directory;
            populatePlaylist();
        }
        #endregion

        #region Methods
        // Nonrecursively checks directory for supported filetypes and creates a playlist out of them
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

        /// <summary>
        /// Gets the currently selected song from the playlist
        /// </summary>
        /// <returns>A fully qualified path to a song file</returns>
        public string getSong()
        {
            return playlist.First.Value;
        }

        /// <summary>
        /// Cycles the playlist forward one song
        /// </summary>
        public void cyclePlaylistForwards()
        {
            string song = playlist.First.Value;
            playlist.RemoveFirst();
            playlist.AddLast(song);
        }

        /// <summary>
        /// Cycles the playlist backwards one song
        /// </summary>
        public void cyclePlaylistBackwards()
        {
            string song = playlist.Last.Value;
            playlist.RemoveLast();
            playlist.AddFirst(song);
        }
        #endregion
    }
}
