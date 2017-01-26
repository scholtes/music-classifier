namespace EmotionalGUI
{
    partial class Canvas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Panel dynamicButtonPanel;
            this.windowControlPanel = new System.Windows.Forms.Panel();
            this.appName = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.albumLabel = new System.Windows.Forms.Label();
            this.artistLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.durationLabel = new System.Windows.Forms.Label();
            this.currentTimeLabel = new System.Windows.Forms.Label();
            this.album = new System.Windows.Forms.Label();
            this.artist = new System.Windows.Forms.Label();
            this.title = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.seekbar = new System.Windows.Forms.ProgressBar();
            this.panel5 = new System.Windows.Forms.Panel();
            this.Settings = new System.Windows.Forms.Button();
            this.nextButton = new System.Windows.Forms.Button();
            this.previousButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.playButton = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.thumbNailPictureBox = new System.Windows.Forms.PictureBox();
            dynamicButtonPanel = new System.Windows.Forms.Panel();
            this.windowControlPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thumbNailPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // dynamicButtonPanel
            // 
            dynamicButtonPanel.AutoScroll = true;
            dynamicButtonPanel.BackColor = System.Drawing.SystemColors.ActiveBorder;
            dynamicButtonPanel.Location = new System.Drawing.Point(0, 40);
            dynamicButtonPanel.Name = "dynamicButtonPanel";
            dynamicButtonPanel.Size = new System.Drawing.Size(500, 350);
            dynamicButtonPanel.TabIndex = 1;
            // 
            // windowControlPanel
            // 
            this.windowControlPanel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.windowControlPanel.Controls.Add(this.appName);
            this.windowControlPanel.Controls.Add(this.closeButton);
            this.windowControlPanel.Location = new System.Drawing.Point(0, 0);
            this.windowControlPanel.Name = "windowControlPanel";
            this.windowControlPanel.Size = new System.Drawing.Size(500, 40);
            this.windowControlPanel.TabIndex = 0;
            this.windowControlPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.windowControlPanel_MouseDown);
            // 
            // appName
            // 
            this.appName.AutoSize = true;
            this.appName.Location = new System.Drawing.Point(43, 13);
            this.appName.Name = "appName";
            this.appName.Size = new System.Drawing.Size(75, 13);
            this.appName.TabIndex = 0;
            this.appName.Text = "Emotional GUI";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(422, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 0;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel3.Controls.Add(this.albumLabel);
            this.panel3.Controls.Add(this.artistLabel);
            this.panel3.Controls.Add(this.titleLabel);
            this.panel3.Controls.Add(this.durationLabel);
            this.panel3.Controls.Add(this.currentTimeLabel);
            this.panel3.Controls.Add(this.album);
            this.panel3.Controls.Add(this.artist);
            this.panel3.Controls.Add(this.title);
            this.panel3.Location = new System.Drawing.Point(0, 390);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 140);
            this.panel3.TabIndex = 2;
            // 
            // albumLabel
            // 
            this.albumLabel.AutoSize = true;
            this.albumLabel.Location = new System.Drawing.Point(65, 47);
            this.albumLabel.Name = "albumLabel";
            this.albumLabel.Size = new System.Drawing.Size(32, 13);
            this.albumLabel.TabIndex = 7;
            this.albumLabel.Text = "xxxxx";
            // 
            // artistLabel
            // 
            this.artistLabel.AutoSize = true;
            this.artistLabel.Location = new System.Drawing.Point(62, 30);
            this.artistLabel.Name = "artistLabel";
            this.artistLabel.Size = new System.Drawing.Size(32, 13);
            this.artistLabel.TabIndex = 6;
            this.artistLabel.Text = "xxxxx";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(62, 13);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(32, 13);
            this.titleLabel.TabIndex = 5;
            this.titleLabel.Text = "xxxxx";
            // 
            // durationLabel
            // 
            this.durationLabel.AutoSize = true;
            this.durationLabel.Location = new System.Drawing.Point(59, 107);
            this.durationLabel.Name = "durationLabel";
            this.durationLabel.Size = new System.Drawing.Size(47, 13);
            this.durationLabel.TabIndex = 4;
            this.durationLabel.Text = "Duration";
            // 
            // currentTimeLabel
            // 
            this.currentTimeLabel.AutoSize = true;
            this.currentTimeLabel.Location = new System.Drawing.Point(23, 107);
            this.currentTimeLabel.Name = "currentTimeLabel";
            this.currentTimeLabel.Size = new System.Drawing.Size(34, 13);
            this.currentTimeLabel.TabIndex = 3;
            this.currentTimeLabel.Text = "00:00";
            // 
            // album
            // 
            this.album.AutoSize = true;
            this.album.Location = new System.Drawing.Point(22, 39);
            this.album.Name = "album";
            this.album.Size = new System.Drawing.Size(36, 13);
            this.album.TabIndex = 2;
            this.album.Text = "Album";
            // 
            // artist
            // 
            this.artist.AutoSize = true;
            this.artist.Location = new System.Drawing.Point(22, 26);
            this.artist.Name = "artist";
            this.artist.Size = new System.Drawing.Size(30, 13);
            this.artist.TabIndex = 1;
            this.artist.Text = "Artist";
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(22, 13);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(27, 13);
            this.title.TabIndex = 0;
            this.title.Text = "Title";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel4.Controls.Add(this.seekbar);
            this.panel4.Location = new System.Drawing.Point(0, 530);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(500, 25);
            this.panel4.TabIndex = 3;
            // 
            // seekbar
            // 
            this.seekbar.Location = new System.Drawing.Point(0, 3);
            this.seekbar.Name = "seekbar";
            this.seekbar.Size = new System.Drawing.Size(500, 19);
            this.seekbar.TabIndex = 5;
            this.seekbar.Click += new System.EventHandler(this.seekBar_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel5.Controls.Add(this.Settings);
            this.panel5.Controls.Add(this.nextButton);
            this.panel5.Controls.Add(this.previousButton);
            this.panel5.Controls.Add(this.stopButton);
            this.panel5.Controls.Add(this.playButton);
            this.panel5.Location = new System.Drawing.Point(0, 555);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(500, 100);
            this.panel5.TabIndex = 4;
            // 
            // Settings
            // 
            this.Settings.Location = new System.Drawing.Point(413, 60);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(75, 23);
            this.Settings.TabIndex = 4;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // nextButton
            // 
            this.nextButton.Location = new System.Drawing.Point(320, 33);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(75, 23);
            this.nextButton.TabIndex = 3;
            this.nextButton.Text = "Next";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // previousButton
            // 
            this.previousButton.Location = new System.Drawing.Point(77, 33);
            this.previousButton.Name = "previousButton";
            this.previousButton.Size = new System.Drawing.Size(75, 23);
            this.previousButton.TabIndex = 2;
            this.previousButton.Text = "Previous";
            this.previousButton.UseVisualStyleBackColor = true;
            this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(239, 33);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 1;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // playButton
            // 
            this.playButton.Location = new System.Drawing.Point(158, 33);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel6.Controls.Add(this.thumbNailPictureBox);
            this.panel6.Location = new System.Drawing.Point(250, 390);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(250, 140);
            this.panel6.TabIndex = 5;
            // 
            // thumbNailPictureBox
            // 
            this.thumbNailPictureBox.Location = new System.Drawing.Point(18, 19);
            this.thumbNailPictureBox.Name = "thumbNailPictureBox";
            this.thumbNailPictureBox.Size = new System.Drawing.Size(206, 101);
            this.thumbNailPictureBox.TabIndex = 0;
            this.thumbNailPictureBox.TabStop = false;
            // 
            // Canvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 650);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(dynamicButtonPanel);
            this.Controls.Add(this.windowControlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Canvas";
            this.Text = "Form1";
            this.windowControlPanel.ResumeLayout(false);
            this.windowControlPanel.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.thumbNailPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel windowControlPanel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label durationLabel;
        private System.Windows.Forms.Label currentTimeLabel;
        private System.Windows.Forms.Label album;
        private System.Windows.Forms.Label artist;
        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button previousButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Label appName;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label albumLabel;
        private System.Windows.Forms.Label artistLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox thumbNailPictureBox;
        private System.Windows.Forms.ProgressBar seekbar;
    }
}

