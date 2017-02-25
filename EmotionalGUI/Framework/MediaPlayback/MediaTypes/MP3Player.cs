
namespace Framework
{

    public class MP3Player : IAudioPlayer
    {
        private WMPLib.WindowsMediaPlayer audioPlayer = null;

        public MP3Player()
        {
            audioPlayer = new WMPLib.WindowsMediaPlayer();
        }

        /// <summary>
        /// Begin Playback of audio file
        /// </summary>
        /// <param name="song">A fully qualified path to an MP3 file</param>
        public void Play(string song)
        {
            audioPlayer.URL = song;
            audioPlayer.controls.play();
        }

        /// <summary>
        /// Resumes playback of an MP3 file
        /// </summary>
        public void Resume()
        {
            audioPlayer.controls.play();
        }

        /// <summary>
        /// Pauses plaback of an MP3 file
        /// </summary>
        public void Pause()
        {
            audioPlayer.controls.pause();
        }

        /// <summary>
        /// Stops plaback of an MP3 file
        /// </summary>
        public void Stop()
        {
            audioPlayer.controls.stop();
        }

        /// <summary>
        /// Seeks to a specific time in an MP3 file
        /// </summary>
        /// <param name="seconds">The number of seconds to begin playback at</param>
        public void setPosition(double seconds)
        {
            audioPlayer.controls.currentPosition = seconds;
        }

        /// <summary>
        /// Changes the volume of the currently playing song
        /// </summary>
        /// <param name="volume">Volume is between 0 and 100</param>
        public void changeVolume(int volume)
        {
            audioPlayer.settings.volume = volume;
        }
    }
}
