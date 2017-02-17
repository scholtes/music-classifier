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

namespace GUI
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        GUIModel model;

        public Settings()
        {
            InitializeComponent();
        }

        public void setModel(GUIModel model)
        {
            this.model = model;
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            var dialogResult = fbd.ShowDialog();
            string newpath = dialogResult.ToString();
            // I know!
            Controls.TextBox box = (Controls.TextBox)(this.FindName("directoryTextBox"));

        }

        private void emptyDatabase_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void classifyLibraryButton_Click(object sender, RoutedEventArgs e)
        {
            model.classifyLibrary();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            this.Hide();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            model.moveWindow(this, e);
        }
    }
}
