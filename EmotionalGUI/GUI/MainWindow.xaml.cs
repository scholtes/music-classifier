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
        MediaControllerButtonUpdater buttonUpdater;

        public MainWindow()
        {
            InitializeComponent();
            buttonUpdater = new MediaControllerButtonUpdater(PreviousControl, PlayControl, PauseControl, StopControl, NextControl);
            mediaController = new MediaController(this, UserSettings.Default.Volume);
            settings = new Settings();
            UpdateButtonStates();
            volumeSlider.Value = UserSettings.Default.Volume;
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
            if (PlayControl.IsEnabled)
            {
                // no autoplay right now because it messes up the control bar
                //mediaController.Play();
            }
        }

        public void UpdateButtonStates()
        {
            bool hasPlaylist = mediaController.HasPlayList;
            PlayControl.IsEnabled = hasPlaylist;
            StopControl.IsEnabled = hasPlaylist;
            PreviousControl.IsEnabled = hasPlaylist;
            NextControl.IsEnabled = hasPlaylist;
            PauseControl.IsEnabled = hasPlaylist;
            //volumeSlider.IsEnabled = hasPlaylist;
            seekbarCursorSlider.IsEnabled = hasPlaylist;

            if (hasPlaylist)
            {
                buttonUpdater.EnableButtons();
            }
            else
            {
                buttonUpdater.DisableButtons();
            }
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
        
        private void UpdatePlayList(List<string> songs)
        {
            List<PlayListItemDTO> pliDTO = new List<PlayListItemDTO>();
            foreach(string song in songs)
            {
                string songname = System.IO.Path.GetFileNameWithoutExtension(song);
                pliDTO.Add(new PlayListItemDTO { Title = songname });
            }
            
            playlistListBox.ItemsSource = pliDTO;
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string song = ((Label)sender).Content.ToString();
            mediaController.PlaySong(song);
        }

        private void closeImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            settings.Close();
            this.Close();
        }

        #region MediaControls

        private void PreviousControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PreviousControl.IsEnabled)
            {
                mediaController.Prev();
                buttonUpdater.UpdateImage(MediaControl.Prev, MAction.MouseDown);
            }
        }
        private void PreviousControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PreviousControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Prev, MAction.Hover);
            }

        }
        private void PreviousControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PreviousControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Prev, MAction.UnHover);
            }

        }
        private void PreviousControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (PreviousControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Prev, MAction.MouseUp);
            }
        }

        private void NextControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (NextControl.IsEnabled)
            {
                mediaController.Next();
                buttonUpdater.UpdateImage(MediaControl.Next, MAction.MouseDown);
            }
        }
        private void NextControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (NextControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Next, MAction.Hover);
            }

        }
        private void NextControl_MouseLeave(object sender, MouseEventArgs e)
        {

            if (NextControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Next, MAction.UnHover);
            }
        }
        private void NextControl_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (NextControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Next, MAction.MouseUp);
            }
        }

        private void StopControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (StopControl.IsEnabled)
            {
                mediaController.Stop();
                buttonUpdater.UpdateImage(MediaControl.Stop, MAction.MouseDown);
            }
        }
        private void StopControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (StopControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Stop, MAction.Hover);
            }

        }
        private void StopControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (StopControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Stop, MAction.UnHover);
            }

        }
        private void StopControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (StopControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Stop, MAction.MouseUp);
            }

        }

        private void PauseControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PauseControl.IsEnabled)
            {
                mediaController.Pause();
                buttonUpdater.UpdateImage(MediaControl.Pause, MAction.MouseDown);
            }
        }
        private void PauseControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PauseControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Pause, MAction.Hover);
            }

        }
        private void PauseControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PauseControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Pause, MAction.UnHover);
            }

        }
        private void PauseControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (PauseControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Pause, MAction.MouseUp);
            }
        }


        private void PlayControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (PlayControl.IsEnabled)
            {
                mediaController.Play();
                buttonUpdater.UpdateImage(MediaControl.Play, MAction.MouseDown);
            }
        }
        private void PlayControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PlayControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Play, MAction.Hover);
            }

        }
        private void PlayControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PlayControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Play, MAction.UnHover);
            }

        }
        private void PlayControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (PlayControl.IsEnabled)
            {
                buttonUpdater.UpdateImage(MediaControl.Play, MAction.MouseUp);
            }

        }

        #endregion

        private void seekbarCursorSlider_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (seekbarCursorSlider.IsEnabled)
            {
                isSeekbarPressed = true;
            }
        }

        private void seekbarCursorSlider_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (seekbarCursorSlider.IsEnabled)
            {
                isSeekbarPressed = false;
                mediaController.Seek(seekbarCursorSlider.Value);
            }
        }
    }
}
