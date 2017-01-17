using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Framework
{
    class MP3 : ITagManager
    {
        // Properties
        private TagLib.Mpeg.AudioFile file = null;
        private string path;

        //Constructors
        public MP3(string songpath)
        {
            path = songpath;
            file = new TagLib.Mpeg.AudioFile(songpath);
        }

        // This should toss an exception as a safety check because songpath is absolutely needed
        private MP3() { throw new Exception("How did you call this?"); }

        //Methods
        public string getTitle()
        {
            return file.Tag.Title;
        }

        public string getAlbum()
        {
            return file.Tag.Album;
        }

        public string getArtist()
        {
            return file.Tag.AlbumArtists[0];
        }

        public Image getThumbnail()
        {
            if (file.Tag.Pictures.Length >= 1)
            {
                var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                return Image.FromStream(new MemoryStream(bin)).GetThumbnailImage(100, 100, null, IntPtr.Zero);
            }
            return null;
        }

        public TimeSpan getDuration()
        {
            return file.Properties.Duration;
        }
    }
}
