using System;
namespace Framework
{
    public interface IDatabase
    {
        /// <summary>
        /// Get a list of X of the closest matching songs, DTO is a coordinate in emotionspace
        /// </summary>
        /// <param name="numberOfSongs">The number of songs to make a playlist out of</param>
        /// <param name="emotionSpaceDTO"></param>
        /// <returns></returns>
        string[] getSongs(int numberOfSongs, EmotionSpaceDTO emotionSpaceDTO);

        /// <summary>
        /// Adds a song to the database
        /// </summary>
        /// <param name="songpath">The location of the song</param>
        /// <param name="emotionSpaceDTO">The coordinates in the emotionspace</param>
        void addSongToDatabase(string songpath, EmotionSpaceDTO emotionSpaceDTO);

        /// <summary>
        /// Remove a song from the database
        /// </summary>
        /// <param name="songpath">A fully qualified path to an audio file</param>
        void removeSongFromDatabase(string songpath);
    }
}
