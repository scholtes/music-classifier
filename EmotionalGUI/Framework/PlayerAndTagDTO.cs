using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class PlayerAndTagDTO
    {
        private PlayerAndTagDTO() { }
        public PlayerAndTagDTO(MetaDataDTO DTO,ITagManager manager)
        {
            DTO.Album.Text = manager.getAlbum();
            DTO.Artist.Text = manager.getArtist();
            DTO.Duration.Text = manager.getDuration().ToString(@"mm\:ss");
            DTO.Thumbnail.Image = manager.getThumbnail();
            DTO.Title.Text = manager.getTitle();
            tagmanager = manager;
        }

        public IAudioPlayer player;
        public ITagManager tagmanager;
    }
}
