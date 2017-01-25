using System;

namespace Framework
{
    /// <summary>
    /// Class used to cast file paths to file types
    /// </summary>
    static class MediaClassifier
    {
        /// <summary>
        /// Gets the music player and tag information for a songpath
        /// </summary>
        /// <param name="songpath">A fully qualified path to a file</param>
        /// <returns>A DTO that contains an audio player and tag retriever</returns>
        static public PlayerAndTagDTO getDTO(string songpath)
        {
            if (String.IsNullOrEmpty(songpath))
            {
                throw new ArgumentException("Songpath is empty");
            }
            if (!System.IO.File.Exists(songpath))
            {
                throw new ArgumentException("File not found");
            }

            if (songpath.ToLower().EndsWith(".mp3"))
            {
                ITagManager tagmanager = new MP3(songpath);
                IAudioPlayer player = new MP3Player();

                return new PlayerAndTagDTO(player, tagmanager);
            }
            else if (songpath.EndsWith(".wma"))
            {
                throw new NotImplementedException(".wma is not an accepted filetype yet");
            }
            else if (songpath.EndsWith(".wav"))
            {
                throw new NotImplementedException(".wav is not an accepted filetype yet");
            }

            throw new Exception("We don't support this file type");

        }
    }
}
