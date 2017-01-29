using System;
using System.Windows.Forms;
using Framework;

namespace EmotionalGUI
{
    public partial class Settings : Form
    {
        GUIModel model;

        public Settings()
        {
            InitializeComponent();
        }

        public void setModel(GUIModel mod)
        {
            model = mod;
        }

        private void closeSettingsButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Hide();
        }

        private void Settings_MouseDown(object sender, MouseEventArgs e)
        {
            model.moveWindow(this, e);
        }

        private void classifyLibraryButton_Click(object sender, EventArgs e)
        {
            model.classifyLibrary();
        }
    }
}
