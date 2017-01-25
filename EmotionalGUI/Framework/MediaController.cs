using System;

namespace Framework
{
    /// <summary>
    /// Enumerations for the current status of the audio player
    /// </summary>
    enum PlayerStatus
    {
        Playing,
        Paused,
        Stopped
    }

    /// <summary>
    /// A class used to control the audio player and write label information
    /// </summary>
    public class MediaController
    {
        #region Properties
        public MetaDataLabelsDTO metadataLabels = null;
        public string directory = null;
        private PlayList playlist = null;
        private PlayerStatus playerstatus = PlayerStatus.Stopped;
        private PlayerAndTagDTO currentPlayerAndTag = null;
        TimeKeeper timer;
        #endregion

        #region Constructors
        private MediaController() { }
        public MediaController(MetaDataLabelsDTO MDD, string dir)
        {
            metadataLabels = MDD;
            directory = dir;
            playlist = new PlayList(dir);
            timer = new TimeKeeper(MDD.Time);
        }
        #endregion

        #region Methods
        public void Play()
        {
            if (playerstatus == PlayerStatus.Stopped)
            {
                string song = playlist.getSong();
                PlayerAndTagDTO data = MediaClassifier.getDTO(song);
                currentPlayerAndTag = data;
                timer.max = data.TagManager.getDuration();
                timer.Start();
                data.AudioPlayer.Play(song);
                playerstatus = PlayerStatus.Playing;
                return;
            }

            if (playerstatus == PlayerStatus.Playing)
            {
                timer.Pause();
                currentPlayerAndTag.AudioPlayer.Pause();
                playerstatus = PlayerStatus.Paused;
                return;
            }

            if (playerstatus == PlayerStatus.Paused)
            {
                timer.Start();
                currentPlayerAndTag.AudioPlayer.Resume();
                playerstatus = PlayerStatus.Playing;
                return;
            }
        }

        public void Stop()
        {
            timer.Reset();
            currentPlayerAndTag.AudioPlayer.Stop();
            playerstatus = PlayerStatus.Stopped;
        }

        public void Prev()
        {
            playlist.cyclePlaylistBackwards();
            this.Stop();
            this.Play();
        }

        public void Next()
        {
            playlist.cyclePlaylistForwards();
            this.Stop();
            this.Play();
        }

        public void Seek(double seconds)
        {
            currentPlayerAndTag.AudioPlayer.setPosition(seconds);
        }

        public double getTotalSeconds()
        {
            TimeSpan timespan = currentPlayerAndTag.TagManager.getDuration();
            return timespan.TotalSeconds;
        }
        #endregion
    }
}
