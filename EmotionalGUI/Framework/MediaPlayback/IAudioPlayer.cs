using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public interface IAudioPlayer
    {
        // Play a song at the given fully qualified path
        void Play(string songpath);
        // Pause the current playing song
        void Pause();
        // Stop the current playing song
        void Stop();
        // Resumes Playback from the paused state
        void Resume();
    }
}
