using System;
using System.Windows.Forms;

namespace Framework
{
    public class GUIModel
    {
        #region Properties
        private Form guiForm;
        private Form settingsForm;
        private MetaDataLabelsDTO metadataLabels;
        private GUIControlDTO guiControls;
        private SettingsControlDTO settingsControls;
        private MediaController mediaController;
        private IDatabase database;
        private IClassifier classifier;
        #endregion

        #region Constructors
        private GUIModel() { }
        public GUIModel(Form gui, Form settings)
        {
            guiForm = gui;
            settingsForm = settings;
            metadataLabels = MetaDataDTOMapper.getMetaDataDTO(gui);
            guiControls = GUIControlDTOMapper.getControlDTO(gui);
            settingsControls = SettingsControlDTOMapper.getSettingsControlDTO(settings);
            mediaController = new MediaController(metadataLabels,guiControls);
            populateDynamicButtons();
            database = ServerDatabase.Instance;
        }
        #endregion

        #region Methods
        public void closeApplication()
        {
            guiForm.Close();
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

        public void moveWindow(Form window, MouseEventArgs e)
        {
            WindowMovement.moveWindow(window, e);
        }

        public void moveSeekbarCursorAlongX(Panel panel,MouseEventArgs e)
        {
            WindowMovement.moveWindowAlongX(panel, e, guiControls.seekbarCursorPanel.Location.X, guiControls.seekbarCursorPanel.Location.X + guiControls.seekbarCursorPanel.Width);
        }

        public void seekBarMoved(int x)
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            double percent = 0;
            int form_width = guiControls.seekbar.Width;
            int form_x_start = guiControls.seekbar.Location.X;
            percent = ((double)x - form_x_start) / form_width;
            mediaController.Seek(percent);
        }

        public void seekBarCursorMoved(int x)
        {
            int test = x - guiForm.Location.X + guiControls.seekbar.Location.X;
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            double percent = 0;
            int form_width = guiControls.seekbar.Width;
            int form_x_start = guiControls.seekbar.Location.X;
            percent = ((double)test - form_x_start) / form_width;
            mediaController.Seek(percent);
        }

        private void populateDynamicButtons()
        {
            ButtonAdder.addButtons(guiControls.dynamicButtonPanel,mediaController.playlist);
        }

        public void classifyLibrary()
        {
            string directory = settingsControls.musicDirectoryTextBox.Text;
            string[] songs = DirectoryBrowser.getSongs(directory);
            classifier = new FakeClassifier();
            string json = classifier.classifySongs(songs);
            JsonDTO data = JsonDTOMapper.getJsonDTO(json);
            Database.addResults(data,database);
        }

        public void showSettings()
        {
            settingsForm.Show();
        }

        #endregion
    }
}
