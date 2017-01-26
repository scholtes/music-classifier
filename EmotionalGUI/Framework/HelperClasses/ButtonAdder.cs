using System.Windows.Forms;
using System;

namespace Framework
{
    /// <summary>
    /// Adds buttons to a panel in an okay looking fashion
    /// </summary>
    public static class ButtonAdder
    {
        public static void addButtons(Panel panel, PlayList playlist)
        {  
            int buttonCount = 0;

            foreach (Button button in Buttons.getButtons(playlist))
            {
                System.Drawing.Point location = new System.Drawing.Point();
                location.Y = Convert.ToInt32(50 * Math.Floor(buttonCount / 2.0));
                location.X = 200 * (buttonCount++ % 2);
                button.Location = location;
                panel.Controls.Add(button);
            }
        }
    }
}
