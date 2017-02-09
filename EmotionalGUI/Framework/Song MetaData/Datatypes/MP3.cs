using System;
using System.IO;
using System.Drawing;

namespace Framework
{
    class MP3 : ITagManager
    {
        #region Properties
        private TagLib.Mpeg.AudioFile file = null;
        private string path;
        #endregion

        #region Constructors
        private MP3() { }
        public MP3(string songpath)
        {
            if (!songpath.ToLower().Contains(".mp3"))
            {
                throw new ArgumentException("File is not an mp3");
            }
            if (String.IsNullOrEmpty(songpath))
            {
                throw new ArgumentException("No songpath given");
            }
            if(!File.Exists(songpath))
            {
                throw new ArgumentException("File not found");
            }
            path = songpath;
            file = new TagLib.Mpeg.AudioFile(songpath);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the title of the mp3
        /// </summary>
        /// <returns>The title</returns>
        public string getTitle()
        {
            try
            {
                return file.Tag.Title;
            }
            catch (Exception)
            {
                return "Unknown Title";
            }
        }

        /// <summary>
        /// Returns the Album of the mp3
        /// </summary>
        /// <returns>The album</returns>
        public string getAlbum()
        {
            try
            {
                return file.Tag.Album;
            }
            catch (Exception)
            {
                return "Unknown Album";
            }
        }

        /// <summary>
        /// Returns the first Artist of the mp3
        /// </summary>
        /// <returns>An artist</returns>
        public string getArtist()
        {
            try
            {
                return file.Tag.AlbumArtists[0];
            }
            catch (Exception)
            {
                return "Unknown Artist";
            }
        }

        /// <summary>
        /// Returns the thumbnail image of an mp3
        /// </summary>
        /// <returns>An image</returns>
        public Image getThumbnail()
        {
            try
            {
                if (file.Tag.Pictures.Length >= 1)
                {
                    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                    return Image.FromStream(new MemoryStream(bin)).GetThumbnailImage(100, 100, null, IntPtr.Zero);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the Duration of a song
        /// </summary>
        /// <returns>The duration</returns>
        public TimeSpan getDuration()
        {
            try
            {
                return file.Properties.Duration;
            }
            catch (Exception)
            {
                return new TimeSpan(0);
            }
        }
        #endregion
    }
}
