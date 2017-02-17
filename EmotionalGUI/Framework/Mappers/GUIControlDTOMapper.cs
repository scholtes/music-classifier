using System.Windows.Forms;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Framework
{
    /// <summary>
    /// Maps a forms controls to a DTO
    /// </summary>
    public static class GUIControlDTOMapper
    {
        public static GUIControlDTO getControlDTO(Window window)
        {
            var dto = new GUIControlDTO()
            {
                //dynamicButtonPanel = form.Controls.Find("dynamicButtonPanel", true).First() as Panel,
                seekbar = (Canvas)(window.FindName("seekbarCanvas")),
                seekbarCursorCanvas = (Canvas)(window.FindName("seekbarCursorCanvas"))
            };
            return dto;
        }
    }
}
