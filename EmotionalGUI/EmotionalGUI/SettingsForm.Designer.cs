namespace EmotionalGUI
{
    partial class Settings
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
            this.closeSettingsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.musicDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.classifyLibraryButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeSettingsButton
            // 
            this.closeSettingsButton.Location = new System.Drawing.Point(199, 50);
            this.closeSettingsButton.Name = "closeSettingsButton";
            this.closeSettingsButton.Size = new System.Drawing.Size(172, 23);
            this.closeSettingsButton.TabIndex = 0;
            this.closeSettingsButton.Text = "Done";
            this.closeSettingsButton.UseVisualStyleBackColor = true;
            this.closeSettingsButton.Click += new System.EventHandler(this.closeSettingsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Music Directory";
            // 
            // musicDirectoryTextBox
            // 
            this.musicDirectoryTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::EmotionalGUI.Properties.Settings.Default, "DirectoryPath", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.musicDirectoryTextBox.Location = new System.Drawing.Point(98, 6);
            this.musicDirectoryTextBox.Name = "musicDirectoryTextBox";
            this.musicDirectoryTextBox.Size = new System.Drawing.Size(273, 20);
            this.musicDirectoryTextBox.TabIndex = 2;
            this.musicDirectoryTextBox.Text = global::EmotionalGUI.Properties.Settings.Default.DirectoryPath;
            // 
            // classifyLibraryButton
            // 
            this.classifyLibraryButton.Location = new System.Drawing.Point(17, 50);
            this.classifyLibraryButton.Name = "classifyLibraryButton";
            this.classifyLibraryButton.Size = new System.Drawing.Size(176, 23);
            this.classifyLibraryButton.TabIndex = 3;
            this.classifyLibraryButton.Text = "Classify Library";
            this.classifyLibraryButton.UseVisualStyleBackColor = true;
            this.classifyLibraryButton.Click += new System.EventHandler(this.classifyLibraryButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 85);
            this.Controls.Add(this.classifyLibraryButton);
            this.Controls.Add(this.musicDirectoryTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeSettingsButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Settings";
            this.Text = "Settings";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Settings_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeSettingsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox musicDirectoryTextBox;
        private System.Windows.Forms.Button classifyLibraryButton;
    }
}