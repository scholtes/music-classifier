using System;
using System.Windows.Forms;

namespace Framework
{
    public class GUIModel
    {
        #region Properties
        private Form target;
        private MetaDataLabelsDTO metadataLabels;
        private ControlDTO controls;
        private MediaController mediaController;
        #endregion

        #region Constructors
        private GUIModel() { }
        public GUIModel(Form form)
        {
            target = form;
            metadataLabels = MetaDataDTOMapper.getMetaDataDTO(form);
            controls = ControlDTOMapper.getControlDTO(form);
            mediaController = new MediaController(metadataLabels,controls);
            populateDynamicButtons();
        }
        #endregion

        #region Methods
        public void closeApplication()
        {
            target.Close();
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
            WindowMovement.moveWindowAlongX(panel, e, controls.seekbarCursorPanel.Location.X, controls.seekbarCursorPanel.Location.X + controls.seekbarCursorPanel.Width);
        }

        public void seekBarMoved(int x)
        {
            if (mediaController.playlist == null || mediaController.playlist.getCount() == 0) { throw new InvalidOperationException("No playlist generated"); }
            double percent = 0;
            int form_width = controls.seekbar.Width;
            int form_x_start = controls.seekbar.Location.X;
            percent = ((double)x - form_x_start) / form_width;
            mediaController.Seek(percent);
        }

        private void populateDynamicButtons()
        {
            ButtonAdder.addButtons(controls.dynamicButtonPanel,mediaController.playlist);
        }
        #endregion
    }
}
