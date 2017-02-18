using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Framework;
using Framework.Interfaces;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainForm 
    {
        public MainWindow()
        {
            InitializeComponent();
            mediaController = new MediaController(this);

            seekbarCursorCanvas.Height = seekbarCanvas.Height;
            seekbarCursorCanvas.Width = seekbarCanvas.Width / 50;
            Canvas.SetLeft(seekbarCursorCanvas, Canvas.GetLeft(seekbarCanvas));
            Canvas.SetTop(seekbarCursorCanvas, Canvas.GetTop(seekbarCanvas));

            UpdateButtonStates();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            mediaController.Prev();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            mediaController.Play();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaController.Stop();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            mediaController.Next();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            Settings settingdlg = new Settings();

            settingdlg.ShowDialog();
            if (settingdlg.DialogResult.HasValue && settingdlg.DialogResult.Value)
            {
                string dir = settingdlg.directoryTextBox.Text;
                if (Directory.Exists(dir))
                {
                    string[] songs = DirectoryBrowser.getSongs(dir);
                    //will start automatically 
                    worker = new ClassifierThread(songs);

                }
                //Handle the settings changed
            }
            UpdateButtonStates();
        }

        private void seekbarCursorCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // TODO
        }

        private void seekbarCursorCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            //TODO
        }

        private void seekbarCursorCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //TODO
        }

        private void seekbarCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double percent = 0;
            double form_width = this.seekbarCanvas.Width;
            double form_x_start = Canvas.GetLeft(this.seekbarCanvas);
            percent = (e.GetPosition(this).X - form_x_start) / form_width;
            mediaController.Seek(percent);
        }

        private void WindowControlCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowMovement.moveWindow(this, e);
        }

        public void updateMetaDataLabels(SongDTO song)
        {
            Album.Content = song.songTag.getAlbum();
            Artist.Content = song.songTag.getArtist();
            this.durationLabel.Content = song.songTag.getDuration().ToString(@"mm\:ss");
            //metadataLabels.Thumbnail.Image = songDTO.songTag.getThumbnail();
            this.titleLabel.Content = song.songTag.getTitle();
            setTimeLabel(new TimeSpan(0));
        }

        public void setTimeLabel(TimeSpan time)
        {
            string result = time.ToString(@"mm\:ss");
            this.currentTimeLabel.Content = result;
        }

        public void UpdateCursor(double percent)
        {
            int x = (int)((seekbarCanvas.Width / 50) * 49 * percent);
            Canvas.SetLeft(seekbarCursorCanvas, x);
        }

        private MediaController mediaController = null;

        private double Positivity
        {
            get
            {
                return this.posSlider.Value;
            }
            set
            {
                this.posSlider.Value = value;
            }
        }

        private double Energy
        {
            get
            {
                return this.energySlider.Value;
            }
            set
            {
                this.energySlider.Value = value;
            }
        }

        private void queryDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            EmotionSpaceDTO point = new EmotionSpaceDTO(Energy, Positivity);
            int numSongs = 20; //Number chosen for now. Should reconsider this. 
            List<string> songs = ServerDatabase.Instance.getSongs(numSongs, point);
            Shuffle(songs);
            mediaController.playlist = new PlayList(songs);
            UpdateButtonStates();
            if (playButton.IsEnabled)
            {
                mediaController.Play();
            }
        }


        public void UpdateButtonStates()
        {
            bool hasPlaylist = mediaController.HasPlayList;
            playButton.IsEnabled = hasPlaylist;
            stopButton.IsEnabled = hasPlaylist;
            previousButton.IsEnabled = hasPlaylist;
            nextButton.IsEnabled = hasPlaylist;
        }

        ClassifierThread worker = null;


        //Randomizer 
        private Random rng = new Random();

        private void Shuffle(IList<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }



    }
}
