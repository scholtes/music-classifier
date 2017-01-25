using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Framework;
using System.Runtime.InteropServices;

namespace EmotionalGUI
{
    public partial class Canvas : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private MediaController player = null;
        private MetaDataLabelsDTO metadataDTO = null;
        Settings settings = null;
        string dir = null;
        int buttonCount = 0;

        public Canvas()
        {
            InitializeComponent();

            metadataDTO = new MetaDataLabelsDTO()
            {
                Album = this.Controls.Find("albumLabel", true).First() as Label,
                Artist = this.Controls.Find("artistLabel", true).First() as Label,
                Duration = this.Controls.Find("durationLabel", true).First() as Label,
                Thumbnail = this.Controls.Find("thumbNailPictureBox", true).First() as PictureBox,
                Title = this.Controls.Find("titleLabel", true).First() as Label,
                Time = this.Controls.Find("currentTime",true).First() as Label

            };
            settings = new Settings();
            settings.Owner = this;
            dir = (settings.Controls.Find("textBox1", true).First() as TextBox).Text;
        }

        // A button to test functionality
        private void button2_Click(object sender, EventArgs e)
        {
            //addButton();
            player = new MediaController(metadataDTO, dir);
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            player.Play();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            player.Next();
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            player.Prev();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Settings_Click(object sender, EventArgs e)
        {
            settings.Show();
        }

        public void refreshSettings_Click()
        {
            dir = (settings.Controls.Find("textBox1", true).First() as TextBox).Text;
        }

        private void addButton()
        {
            Button button = new Button();
            button.Click += (s, e) => { System.Console.Beep(); };
            button.Text = "Test " + buttonCount.ToString();
            Point location = new Point();
            location.Y = Convert.ToInt32(50 * Math.Floor(buttonCount / 2.0));
            location.X = 200 * (buttonCount++ % 2);
            button.Location = location;
            Panel pan = this.Controls.Find("panel2", true).First() as Panel;
            pan.Controls.Add(button);

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            Fraction frac = new Fraction();
            frac.numerator = ((MouseEventArgs)e).X - ((ProgressBar)sender).Location.X;
            frac.denominator = ((ProgressBar)sender).Width;
            double seconds = frac.estimateFraction() * player.getTotalSeconds();
            player.Seek(seconds);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
