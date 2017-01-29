using System.Linq;
using System.Windows.Forms;

namespace Framework
{
    /// <summary>
    /// Maps a settingsForm controls to a DTO
    /// </summary>
    public static class SettingsControlDTOMapper
    {
        public static SettingsControlDTO getSettingsControlDTO(Form form)
        {
            var dto = new SettingsControlDTO()
            {
                musicDirectoryTextBox = form.Controls.Find("musicDirectoryTextBox", true).First() as TextBox
            };
            return dto;
        }
    }
}
