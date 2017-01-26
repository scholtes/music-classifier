using System.Windows.Forms;
using System.Linq;

namespace Framework
{
    static public class MetaDataDTOMapper
    {
        static public MetaDataLabelsDTO getMetaDataDTO(Form form)
        {
            var DTO = new MetaDataLabelsDTO()
            {
                Album = form.Controls.Find("albumLabel", true).First() as Label,
                Artist = form.Controls.Find("artistLabel", true).First() as Label,
                Duration = form.Controls.Find("durationLabel", true).First() as Label,
                Thumbnail = form.Controls.Find("thumbNailPictureBox", true).First() as PictureBox,
                Title = form.Controls.Find("titleLabel", true).First() as Label,
                Time = form.Controls.Find("currentTimeLabel", true).First() as Label
            };
            return DTO;
        }
    }
}
