using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Framework
{
    public class MetaDataDTO
    {
        public Label Title { get; set; }
        public Label Album { get; set; }
        public Label Artist { get; set; }
        public Label Duration { get; set; }
        public PictureBox Thumbnail { get; set; } 
    }
}
