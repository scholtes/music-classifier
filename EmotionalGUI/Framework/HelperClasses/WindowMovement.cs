using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Framework
{
    /// <summary>
    /// A class used to create the ability of borderless windows to move when dragged
    /// </summary>
    public static class WindowMovement
    {
        #region Properties
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        #region Constructors
        /// <summary>
        /// Don't ask how this works, it just does
        /// </summary>
        /// <param name="sender">The Form that will be moving</param>
        /// <param name="e">Mouse arguments</param>
        public static void moveWindow(Form sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(sender.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public static void moveWindowAlongX(Panel panel, MouseEventArgs e, int min, int max)
        {
            if (e.Button == MouseButtons.Left) panel.Location = e.Location;
        }

        public static void other(Panel panel, MouseEventArgs e, int min, int max)
        {
            panel.Left += e.X - panel.Location.X;

            panel.Top += e.Y - panel.Location.Y;
        }
        #endregion
    }
}
