using System.Windows.Forms;
using System.Linq;

namespace Framework
{
    /// <summary>
    /// Maps a forms controls to a DTO
    /// </summary>
    public static class GUIControlDTOMapper
    {
        public static GUIControlDTO getControlDTO(Form form)
        {
            var dto = new GUIControlDTO()
            {
                dynamicButtonPanel = form.Controls.Find("dynamicButtonPanel", true).First() as Panel,
                seekbar = form.Controls.Find("seekbar", true).First() as ProgressBar,
                seekbarCursorPanel = form.Controls.Find("seekbarCursorPanel",true).First() as Panel
            };
            return dto;
        }
    }
}
