using System;
using System.Linq;
using System.Windows.Forms;
using Framework;

namespace EmotionalGUI
{
    public partial class Canvas : Form
    {
        Settings settings = null;
        string dir = null;

        GUIModel model;

        public Canvas()
        {
            InitializeComponent();
            settings = new Settings();
            settings.Owner = this;
            model = new GUIModel(this);
            //dir = (settings.Controls.Find("textBox1", true).First() as TextBox).Text;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            model.playSong();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            model.stopSong();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            model.playNextSong();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            model.playPrevSong();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            model.closeApplication();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            settings.Show();
        }

        public void refreshSettings_Click()
        {
            dir = (settings.Controls.Find("textBox1", true).First() as TextBox).Text;
        }

        private void windowControlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            model.moveWindow(this, e);
        }

        private void seekBar_Click(object sender, EventArgs e)
        {
            model.seekBarMoved(((MouseEventArgs)e).X);
        }
    }
}
