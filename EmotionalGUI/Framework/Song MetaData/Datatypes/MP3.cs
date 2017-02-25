using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Framework
{
    class MP3 : BaseAudioFile
    {
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
            if (!File.Exists(songpath))
            {
                throw new ArgumentException("File not found");
            }
            path = songpath;
            file = new TagLib.Mpeg.AudioFile(songpath);

            setTitle();
            setAlbum();
            setArtist();
            setThumbnail();
            setDuration();
        }
        
        sealed protected override void setTitle()
        {
            try
            {
                title = file.Tag.Title;
            }
            catch (Exception)
            {
                title = "Unknown Title";
            }
        }

        sealed protected override void setAlbum()
        {
            try
            {
                album = file.Tag.Album;
            }
            catch (Exception)
            {
                album = "Unknown Album";
            }
        }

        sealed protected override void setArtist()
        {
            try
            {
                artist = file.Tag.AlbumArtists[0];
            }
            catch (Exception)
            {
                artist = "Unknown Artist";
            }
        }

        sealed protected override void setThumbnail()
        {
            try
            {
                if (file.Tag.Pictures.Length >= 1)
                {
                    var bin = (byte[])(file.Tag.Pictures[0].Data.Data);
                    thumbnailSource = BitmapFrame.Create(new MemoryStream(bin), BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    return;
                }
                thumbnailSource = null;
            }
            catch (Exception)
            {
                thumbnailSource = null;
            }
        }

        sealed protected override void setDuration()
        {
            try
            {
                duration = file.Properties.Duration;
            }
            catch (Exception)
            {
                duration = new TimeSpan(0);
            }
        }
    }
}
