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
        public PlayList playlist = null;
        private PlayerStatus playerstatus = PlayerStatus.Stopped;
        private SongDTO songDTO = null;
        TimeKeeper timer;
        #endregion

        #region Constructors
        private MediaController() { }
        public MediaController(MetaDataLabelsDTO MDD,GUIControlDTO C)
        {
            metadataLabels = MDD;
            timer = new TimeKeeper(MDD.Time,C);
            playlist = new PlayList();
        }
        #endregion

        #region Methods
        public void Play()
        {
            if (playerstatus == PlayerStatus.Stopped)
            {
                string song = playlist.getSong();
                songDTO = SongDTOMapper.getSongDTO(song);
                timer.max = songDTO.songTag.getDuration();
                timer.Start();
                updateMetaDataLabels();
                songDTO.songPlayer.Play(song);
                playerstatus = PlayerStatus.Playing;
                return;
            }

            if (playerstatus == PlayerStatus.Playing)
            {
                timer.Pause();
                setTimeLabel(timer.getAccumulatedTime());
                songDTO.songPlayer.Pause();
                playerstatus = PlayerStatus.Paused;
                return;
            }

            if (playerstatus == PlayerStatus.Paused)
            {
                timer.Start();
                songDTO.songPlayer.Resume();
                playerstatus = PlayerStatus.Playing;
                return;
            }
        }

        public void Stop()
        {
            timer.Reset();
            setTimeLabel(new TimeSpan(0));
            songDTO.songPlayer.Stop();
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

        public void Seek(double percent)
        {
            double seconds = percent * songDTO.songTag.getDuration().TotalSeconds;
            TimeSpan time = new TimeSpan(0, (int)seconds / 60, (int)seconds % 60);
            timer.setAccumulatedTime(time);
            songDTO.songPlayer.setPosition(seconds);
            playerstatus = PlayerStatus.Paused;
        }

        private void updateMetaDataLabels()
        {
            metadataLabels.Album.Content = songDTO.songTag.getAlbum();
            metadataLabels.Artist.Content = songDTO.songTag.getArtist();
            metadataLabels.Duration.Content = songDTO.songTag.getDuration().ToString(@"mm\:ss");
            //metadataLabels.Thumbnail.Image = songDTO.songTag.getThumbnail();
            metadataLabels.Title.Content = songDTO.songTag.getTitle();
            setTimeLabel(new TimeSpan(0));
        }

        private void setTimeLabel(TimeSpan time)
        {
            string result = time.ToString(@"mm\:ss");
            metadataLabels.Time.Content = result;
        }
        #endregion
    }
}
