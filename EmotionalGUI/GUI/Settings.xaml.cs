using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Controls = System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Framework;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            directoryTextBox.Text = UserSettings.Default.MusicDirectory;
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dialogResult = fbd.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                this.directoryTextBox.Text = fbd.SelectedPath;
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowMovement.moveWindow(this, e);
        }

        private void ClassifyLibrary_Click(object sender, RoutedEventArgs e)
        {
            string dir = directoryTextBox.Text;
            if (Directory.Exists(dir))
            {
                string[] songs = DirectoryBrowser.getSongs(dir);
                //will start automatically 
                Framework.ClassifierThread worker = new ClassifierThread(songs);
                
            }
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
            this.Hide();
        }

        private void test_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowMovement.moveWindow(this, e);
        }

        private void SaveSettings()
        {
            UserSettings.Default.MusicDirectory = directoryTextBox.Text;
            UserSettings.Default.Save();
        }
    }
}
