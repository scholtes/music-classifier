using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace Framework
{
    /// <summary>
    /// Maps a settingsForm controls to a DTO
    /// </summary>
    public static class SettingsControlDTOMapper
    {
        public static SettingsControlDTO getSettingsControlDTO(Window window)
        {
            var dto = new SettingsControlDTO()
            {
                musicDirectoryTextBox = (TextBox)(window.FindName("musicDirectoryTextBox"))
            };
            return dto;
        }
    }
}
