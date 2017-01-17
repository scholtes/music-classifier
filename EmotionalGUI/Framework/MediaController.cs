using System;
using System.IO;
using System.Collections.Generic;

namespace Framework
{
    enum PlayerStatus
    {
        Playing,
        Paused,
        Stopped
    }

    public class MediaController
    {
        public MetaDataDTO metadataLabels = null;
        public string directory = null;
        private PlayList playlist = null;
        private PlayerStatus playerstatus = PlayerStatus.Stopped;
        private PlayerAndTagDTO currentPlayerAndTag = null;

        private MediaController() { throw new Exception("How did you call this?"); }
        public MediaController(MetaDataDTO MDD, string dir)
        {
            metadataLabels = MDD;
            directory = dir;
            playlist = new PlayList(dir);
        }

        public void Play()
        {
            if(playerstatus == PlayerStatus.Stopped)
            {
                string song = playlist.getCurrentSong();
                PlayerAndTagDTO data = MediaClassifier.getDTO(metadataLabels,song);
                currentPlayerAndTag = data;
                data.player.Play(song);
                playerstatus = PlayerStatus.Playing;
                return;
            }

            if(playerstatus == PlayerStatus.Playing)
            {
                currentPlayerAndTag.player.Pause();
                playerstatus = PlayerStatus.Paused;
                return;
            }

            if(playerstatus == PlayerStatus.Paused)
            {
                string song = playlist.getCurrentSong();
                currentPlayerAndTag.player.Resume();
                playerstatus = PlayerStatus.Playing;return;
            }
        }

        public void Stop()
        {
            currentPlayerAndTag.player.Stop();
            playerstatus = PlayerStatus.Stopped;
        }

        public void Prev()
        {
            string song = playlist.getPrevSong();
            PlayerAndTagDTO data = MediaClassifier.getDTO(metadataLabels,song);
            currentPlayerAndTag.player.Stop();
            currentPlayerAndTag = data;
            data.player.Play(song);
            playerstatus = PlayerStatus.Playing;
        }

        public void Next()
        {
            string song = playlist.getNextSong();
            PlayerAndTagDTO data = MediaClassifier.getDTO(metadataLabels,song);
            currentPlayerAndTag.player.Stop();
            currentPlayerAndTag = data;
            data.player.Play(song);
            playerstatus = PlayerStatus.Playing;
        }
    }
}
