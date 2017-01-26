using System;
using System.Windows.Forms;

namespace EmotionalGUI
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void closeSettingsButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Canvas parent = this.Owner as Canvas;
            parent.refreshSettings_Click();
            this.Hide();
        }
    }
}
