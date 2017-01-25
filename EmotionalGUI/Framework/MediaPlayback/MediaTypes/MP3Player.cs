
namespace Framework
{

    public class MP3Player : IAudioPlayer
    {
        #region Properties
        WMPLib.WindowsMediaPlayer audioPlayer = null;
        #endregion

        #region Constructors
        public MP3Player()
        {
            audioPlayer = new WMPLib.WindowsMediaPlayer();
        }
        #endregion

        #region Methods
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
        #endregion
    }
}
