using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // get the album art
        System.Drawing.Image getThumbnail();

        // get the song duration
        TimeSpan getDuration();
    }
}
