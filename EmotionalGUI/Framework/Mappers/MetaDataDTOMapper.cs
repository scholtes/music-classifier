using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Framework
{
    static public class MetaDataDTOMapper
    {
        static public MetaDataLabelsDTO getMetaDataDTO(Window form)
        {
            var DTO = new MetaDataLabelsDTO()
            {
                Album = (Label)(form.FindName("albumLabel")),
                Artist = (Label)(form.FindName("artistLabel")),
                Duration = (Label)(form.FindName("durationLabel")),
                //Thumbnail = (PictureBox)(form.FindName("thumbNailPictureBox")),
                Title = (Label)(form.FindName("titleLabel")),
                Time = (Label)(form.FindName("currentTimeLabel"))
            };
            return DTO;
        }
    }
}
