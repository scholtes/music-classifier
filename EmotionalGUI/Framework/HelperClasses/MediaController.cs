using System;
using Framework.Interfaces;

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
        public IMainForm mainform = null;
        public PlayList playlist = null;
        private PlayerStatus playerstatus = PlayerStatus.Stopped;
        private SongDTO songDTO = null;
        TimeKeeper timer;
        #endregion

        #region Constructors
        private MediaController() { }
        public MediaController(IMainForm MDD)
        {
            mainform = MDD;
            timer = new TimeKeeper(MDD);
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
                mainform.updateMetaDataLabels(songDTO);
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
            mainform.setTimeLabel(new TimeSpan(0));
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
            if (songDTO != null)
            {
                double seconds = percent * songDTO.songTag.getDuration().TotalSeconds;
                TimeSpan time = new TimeSpan(0, (int)seconds / 60, (int)seconds % 60);
                timer.setAccumulatedTime(time);
                songDTO.songPlayer.setPosition(seconds);
                playerstatus = PlayerStatus.Paused;
            }
        }

        private void setTimeLabel(TimeSpan time)
        {
            mainform.setTimeLabel(time);
        }

        public bool HasPlayList
        {
            get
            {
                bool ret = false;
                if (playlist != null && playlist.getCount() > 0)
                {
                    ret = true;
                }
                return ret;
            }
        }

        #endregion
    }
}
