using System;
using System.IO;
using System.Collections.Generic;

namespace Framework
{
    public class PlayList
    {
        #region Properties
        private LinkedList<string> playlist = null;
        #endregion

        #region Constructors
        private PlayList() { }

        public PlayList(List<string> songs)
        {
            this.loadSongs(songs.ToArray());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads songs into the playlist, will overwrite current playlist
        /// </summary>
        /// <param name="songs">An array of fully qualified song paths</param>
        public void loadSongs(string[] songs)
        {
            playlist = new LinkedList<string>();
            if(songs == null)
            {
                throw new ArgumentException("Cannot pass in null or 0 songs");
            }
            foreach (string song in songs)
            {
                if (string.IsNullOrEmpty(song))
                {
                    continue;
                }
                playlist.AddLast(song);
            }
        }

        /// <summary>
        /// Gets the currently selected song from the playlist
        /// </summary>
        /// <returns>A fully qualified path to a song file</returns>
        public string getSong()
        {
            if(playlist == null || playlist.Count == 0) { throw new NullReferenceException("No Playlist loaded"); }
            return playlist.First.Value;
        }

        /// <summary>
        /// Cycles the playlist forward one song
        /// </summary>
        public void cyclePlaylistForwards()
        {
            if (playlist == null || playlist.Count == 0) { throw new NullReferenceException("No Playlist loaded"); }
            string song = playlist.First.Value;
            playlist.RemoveFirst();
            playlist.AddLast(song);
        }

        /// <summary>
        /// Cycles the playlist backwards one song
        /// </summary>
        public void cyclePlaylistBackwards()
        {
            if (playlist == null || playlist.Count == 0) { throw new NullReferenceException("No Playlist loaded"); }
            string song = playlist.Last.Value;
            playlist.RemoveLast();
            playlist.AddFirst(song);
        }

        /// <summary>
        /// Get the number of songs in the playlist
        /// </summary>
        /// <returns>Number of songs</returns>
        public int getCount()
        {
            if (playlist == null) { throw new NullReferenceException("No Playlist loaded"); }
            return playlist.Count;
        }
        #endregion
    }
}
