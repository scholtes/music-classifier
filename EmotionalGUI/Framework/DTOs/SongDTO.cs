
namespace Framework
{
    /// <summary>
    /// A DTO to hold the song controls and tag informaton
    /// </summary>
    public class SongDTO
    {
        public ITagManager songTag;
        public IAudioPlayer songPlayer;
    }
}
