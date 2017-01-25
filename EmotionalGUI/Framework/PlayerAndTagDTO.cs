
namespace Framework
{
    public class PlayerAndTagDTO
    {
        public IAudioPlayer AudioPlayer;
        public ITagManager TagManager;
        
        private PlayerAndTagDTO() { }
        public PlayerAndTagDTO(IAudioPlayer player,ITagManager manager)
        {
            AudioPlayer = player;
            TagManager = manager;
        }
    }
}
