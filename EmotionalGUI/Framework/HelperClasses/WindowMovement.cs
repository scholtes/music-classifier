using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Framework
{
    /// <summary>
    /// A class used to create the ability of borderless windows to move when dragged
    /// </summary>
    public class WindowMovement
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
        private WindowMovement() { }
        /// <summary>
        /// Don't ask how this works, it just does
        /// </summary>
        /// <param name="sender">The Form that will be moving</param>
        /// <param name="e">Mouse arguments</param>
        public WindowMovement(Form sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(sender.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion
    }
}
