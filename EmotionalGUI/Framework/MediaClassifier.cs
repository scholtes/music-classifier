using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    static class MediaClassifier
    {
        static public PlayerAndTagDTO getDTO(MetaDataDTO mdDTO,string songpath)
        {
            if (songpath.EndsWith(".mp3"))
            {
                ITagManager tagmanager = new MP3(songpath);
                var result = new PlayerAndTagDTO(mdDTO,tagmanager)
                {
                    player = new MP3Player()
                };
                return result;
            }
            else if (songpath.EndsWith(".wma"))
            {
                throw new NotImplementedException(".wma is not an accepted filetype");
            }
            else if (songpath.EndsWith(".wav"))
            {
                throw new NotImplementedException(".wav is not an accepted filetype");
            }

            throw new Exception("We don't support this file type");

        }
    }
}
