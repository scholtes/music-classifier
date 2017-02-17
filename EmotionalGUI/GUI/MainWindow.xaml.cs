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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GUIModel model;

        public MainWindow()
        {
            InitializeComponent();
            Settings settings = new Settings();

            model = new GUIModel(this, settings);
            settings.setModel(model);
            
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            model.closeApplication();
        }
        
        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            model.playPrevSong();
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            model.playSong();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            model.stopSong();
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            model.playNextSong();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            model.showSettings();
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
            model.seekBarMoved(e.GetPosition(this).X);
        }

        private void WindowControlCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            model.moveWindow(this, e);
        }
    }
}
