using System.Collections.Generic;
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
        private PlayList playlist = null;
        private PlayerStatus playerstatus = PlayerStatus.Stopped;
        private SongDTO songDTO = null;
        private int playerVolume;
        TimeKeeper timer;
        #endregion

        #region Constructors
        public MediaController(IMainForm MDD,int initialVolume)
        {
            mainform = MDD;
            timer = new TimeKeeper(MDD);
            playerVolume = initialVolume;
        }

        #endregion

        #region Methods
        public void Play()
        {
            if (playerstatus == PlayerStatus.Stopped)
            {
                string song = playlist.getSong();
                songDTO = SongDTOMapper.getSongDTO(song);
                timer.max = songDTO.songTag.Duration;
                timer.Start();
                mainform.updateSongMetadataInformation(songDTO);
                songDTO.songPlayer.changeVolume(playerVolume);
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
                double seconds = percent * songDTO.songTag.Duration.TotalSeconds;
                TimeSpan time = new TimeSpan(0, (int)seconds / 60, (int)seconds % 60);
                timer.setAccumulatedTime(time);
                songDTO.songPlayer.setPosition(seconds);
                playerstatus = PlayerStatus.Paused;
            }
        }

        public void ChangeVolume(int volume)
        {
            playerVolume = volume;
            songDTO.songPlayer.changeVolume(volume);
        }

        private void setTimeLabel(TimeSpan time)
        {
            mainform.setTimeLabel(time);
        }

        public void PlaySong(string song)
        {
            bool matchingSong = false;
            int count = 0;
            while (!matchingSong)
            {
                if (count > 100) return;
                string currSong = playlist.getSong();
                if (currSong.Contains(song))
                {
                    matchingSong = true;
                    Stop();
                    Play();
                    return;
                }
                playlist.cyclePlaylistForwards();
                count++;
            }
        }

        public void LoadSongs(string[] songs)
        {
            playlist = new PlayList(songs);
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
