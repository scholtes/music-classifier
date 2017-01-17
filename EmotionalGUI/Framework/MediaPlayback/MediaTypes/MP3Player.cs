using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{

    public class MP3Player : IAudioPlayer
    {
        // Properties
        WMPLib.WindowsMediaPlayer audioPlayer = null;

        // Constructors
        public MP3Player()
        {
            audioPlayer = new WMPLib.WindowsMediaPlayer();
        }

        // Methods
        public void Play(string song)
        {
            audioPlayer.URL = song;
            audioPlayer.controls.play();
        }

        public void Resume()
        {
            audioPlayer.controls.play();
        }

        public void Pause()
        {
            audioPlayer.controls.pause();
        }

        public void Stop()
        {
            audioPlayer.controls.stop();
        }
    }
}
