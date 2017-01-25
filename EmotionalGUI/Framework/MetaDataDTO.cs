using System.Windows.Forms;

namespace Framework
{
    /// <summary>
    /// DTO used to hold the labels for the song metadata
    /// </summary>
    public class MetaDataLabelsDTO
    {
        public Label Title { get; set; }
        public Label Album { get; set; }
        public Label Artist { get; set; }
        public Label Duration { get; set; }
        public PictureBox Thumbnail { get; set; } 
        public Label Time { get; set; }
    }
}
