using System;

namespace Framework
{
    public interface ITagManager
    {
        // Get the song title
        string getTitle();

        // Get the song album
        string getAlbum();

        // Get the song artist
        string getArtist();

        // Get the album art
        System.Drawing.Image getThumbnail();

        // Get the song duration
        TimeSpan getDuration();
    }
}
