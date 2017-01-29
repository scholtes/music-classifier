using System;
using System.Windows.Forms;
using Framework;


using System.Drawing;
namespace EmotionalGUI
{
    public partial class GUI : Form
    {
        Point MouseDownLocation;
        GUIModel model;

        public GUI()
        {
            InitializeComponent();
            Settings settings = new EmotionalGUI.Settings();
            settings.Owner = this;

            model = new GUIModel(this,settings);
            settings.setModel(model);
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
            model.showSettings();
        }

        private void windowControlPanel_MouseDown(object sender, MouseEventArgs e)
        {
            model.moveWindow(this, e);
        }

        private void seekBar_Click(object sender, EventArgs e)
        {
            model.seekBarMoved(((MouseEventArgs)e).X);
        }
        
        private void seekbarCursorPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void seekbarCursorPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ((Panel)sender).Left = e.X + ((Panel)sender).Left - MouseDownLocation.X;
            }
        }

        private void seekbarCursorPanel_MouseUp(object sender, MouseEventArgs e)
        {
            model.seekBarCursorMoved(Cursor.Position.X);
        }
    }
}
