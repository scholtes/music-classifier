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
using System.Text.RegularExpressions;
using System.IO;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainForm 
    {
        private MediaController mediaController = null;
        private Random rng = new Random();
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
        Settings settings;
        bool isSeekbarPressed = false;

        public MainWindow()
        {
            InitializeComponent();
            mediaController = new MediaController(this, UserSettings.Default.Volume);
            settings = new Settings();
            UpdateButtonStates();
            volumeSlider.Value = UserSettings.Default.Volume;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            settings.Close();
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
            settings.Show();
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

        public void updateSongMetadataInformation(SongDTO song)
        {
            this.albumLabel.Content = song.songTag.Album;
            this.artistLabel.Content = song.songTag.Artist;
            this.durationLabel.Content = song.songTag.Duration.ToString(@"mm\:ss");
            this.thumbnailImage.Source = song.songTag.ThumbnailSource;
            this.titleLabel.Content = song.songTag.Title;
            setTimeLabel(new TimeSpan(0));
        }

        public void setTimeLabel(TimeSpan time)
        {
            string result = time.ToString(@"mm\:ss");
            this.currentTimeLabel.Content = result;
        }

        public void UpdateCursor(double percent)
        {
            if (seekbarCursorSlider.IsEnabled && !isSeekbarPressed)
            {
                seekbarCursorSlider.Value = percent;
            }
        }

        private void queryDatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            EmotionSpaceDTO point = new EmotionSpaceDTO(Energy, Positivity);
            int numSongs = 20; //Number chosen for now. Should reconsider this. 
            List<string> songs = ServerDatabase.Instance.getSongs(numSongs, point);
            Shuffle(songs);
            UpdatePlayList(songs);
            mediaController.LoadSongs(songs.ToArray());
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
            volumeSlider.IsEnabled = hasPlaylist;
            seekbarCursorSlider.IsEnabled = hasPlaylist;
        }

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

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (volumeSlider.IsEnabled)
            {
                mediaController.ChangeVolume((int)e.NewValue);
                UserSettings.Default.Volume = (int)e.NewValue;
                UserSettings.Default.Save();
            }
        }

        private void seekbarCursorSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (seekbarCursorSlider.IsEnabled)
            {
                isSeekbarPressed = false;
                mediaController.Seek(seekbarCursorSlider.Value);
            }
        }

        private void seekbarCursorSlider_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (seekbarCursorSlider.IsEnabled)
            {
                isSeekbarPressed = true;
            }
        }
        
        private void UpdatePlayList(List<string> songs)
        {
            List<PlayListItemDTO> pliDTO = new List<PlayListItemDTO>();
            foreach(string song in songs)
            {
                var myReg = new Regex(@"\([^\.]+");
                Match match = myReg.Match(song);
                string filteredSongName = match.Value;
                pliDTO.Add(new PlayListItemDTO { Title = filteredSongName });
            }
            
            playlistListBox.ItemsSource = pliDTO;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string song = ((Label)sender).Content.ToString();
            mediaController.PlaySong(song);
        }
    }
}
