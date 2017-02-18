using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Framework
{
    public class GUIModel
    {/*
        #region Properties
        private Window guiWindow;
        private MetaDataLabelsDTO metadataLabels;
        private SettingsControlDTO settingsControls;
        private MediaController mediaController;
        private IDatabase database;
        private IClassifier classifier;
        #endregion

        #region Constructors
        public GUIModel(Window gui)
        {
            guiWindow = gui;
            metadataLabels = MetaDataDTOMapper.getMetaDataDTO(gui);
            //settingsControls = SettingsControlDTOMapper.getSettingsControlDTO(settings);
            //mediaController = new MediaController(metadataLabels,guiControls);
            database = ServerDatabase.Instance;
        }
        #endregion

        #region Methods
        public void closeApplication()
        {
            guiWindow.Close();
        }

        public void playSong()
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            mediaController.Play();
        }

        public void stopSong()
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            mediaController.Stop();
        }

        public void playNextSong()
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            mediaController.Next();
        }

        public void playPrevSong()
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            mediaController.Prev();
        }

        public void moveWindow(Window window, MouseButtonEventArgs e)
        {
            WindowMovement.moveWindow(window, e);
        }

        public void moveSeekbarCursorAlongX(Canvas canvas,MouseButtonEventArgs e)
        {
            //TODO
            //WindowMovement.moveWindowAlongX(canvas, e, Canvas.GetLeft(guiControls.seekbarCursorCanvas), Canvas.GetLeft(guiControls.seekbarCursorCanvas) + guiControls.seekbarCursorCanvas.Width);
        }

        public void seekBarMoved(double x)
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            double percent = 0;
            double form_width = guiControls.seekbar.Width;
            double form_x_start = Canvas.GetLeft(guiControls.seekbar);
            percent = (x - form_x_start) / form_width;
            mediaController.Seek(percent);
        }

        public void seekBarCursorMoved(int x)
        {
            double test = x - guiWindow.Left + Canvas.GetLeft(guiControls.seekbar);
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            double percent = 0;
            double form_width = guiControls.seekbar.Width;
            double form_x_start = Canvas.GetLeft(guiControls.seekbar);
            percent = ((double)test - form_x_start) / form_width;
            mediaController.Seek(percent);
        }

        public void classifyLibrary()
        {
            string directory = settingsControls.musicDirectoryTextBox.Text;
            string[] songs = DirectoryBrowser.getSongs(directory);
            classifier = new Classifier();
            string json = classifier.classifySongs(songs);
            JsonDTO data = JsonDTOMapper.getJsonDTO(json);
            Database.addResults(data,database);
        }

        public void showSettings()
        {
            settingsWindow.ShowDialog();
            if (settingsWindow.DialogResult.HasValue && settingsWindow.DialogResult.Value)
            {
                //Update settings
            }
        }

        #endregion
    }*/
    }
}
