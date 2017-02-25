using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Framework
{
    public abstract class BaseAudioFile
    {
        protected TagLib.Mpeg.AudioFile file;
        protected string path;
        protected string title;
        protected string album;
        protected string artist;
        protected ImageSource thumbnailSource;
        protected TimeSpan duration;

        public string Title
        {
            get { return title; }
            set { }
        }
        public string Album
        {
            get { return album; }
            set { }
        }
        public string Artist
        {
            get { return artist; }
            set { }
        }
        public ImageSource ThumbnailSource
        {
            get { return thumbnailSource; }
            set { }
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set { }
        }

        protected abstract void setTitle();
        protected abstract void setAlbum();
        protected abstract void setArtist();
        protected abstract void setThumbnail();
        protected abstract void setDuration();
    }
}
