using System.Windows.Forms;
using System.Linq;

namespace Framework
{
    /// <summary>
    /// Maps a forms controls to a DTO
    /// </summary>
    public static class ControlDTOMapper
    {
        public static ControlDTO getControlDTO(Form form)
        {
            var dto = new ControlDTO()
            {
                dynamicButtonPanel = form.Controls.Find("dynamicButtonPanel", true).First() as Panel,
                seekbar = form.Controls.Find("seekbar", true).First() as ProgressBar
            };
            return dto;
        }
    }
}
