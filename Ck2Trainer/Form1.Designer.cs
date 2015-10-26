namespace Ck2Trainer
{
    partial class Form1
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
            this.FilesListBox = new System.Windows.Forms.ListBox();
            this.LabelAvailableFiles = new System.Windows.Forms.Label();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogList = new System.Windows.Forms.ListBox();
            this.ClearLog = new System.Windows.Forms.Button();
            this.MoraleToZero = new System.Windows.Forms.Button();
            this.PlayerMoraleTo1 = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ListFiles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FilesListBox
            // 
            this.FilesListBox.FormattingEnabled = true;
            this.FilesListBox.HorizontalScrollbar = true;
            this.FilesListBox.Location = new System.Drawing.Point(12, 74);
            this.FilesListBox.Name = "FilesListBox";
            this.FilesListBox.Size = new System.Drawing.Size(345, 420);
            this.FilesListBox.TabIndex = 0;
            // 
            // LabelAvailableFiles
            // 
            this.LabelAvailableFiles.AutoSize = true;
            this.LabelAvailableFiles.Location = new System.Drawing.Point(12, 26);
            this.LabelAvailableFiles.Name = "LabelAvailableFiles";
            this.LabelAvailableFiles.Size = new System.Drawing.Size(102, 13);
            this.LabelAvailableFiles.TabIndex = 1;
            this.LabelAvailableFiles.Text = "Available Save Files";
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(11, 500);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(914, 20);
            this.ProgressBar.TabIndex = 4;
            // 
            // LogList
            // 
            this.LogList.FormattingEnabled = true;
            this.LogList.Location = new System.Drawing.Point(471, 9);
            this.LogList.Name = "LogList";
            this.LogList.Size = new System.Drawing.Size(453, 472);
            this.LogList.TabIndex = 5;
            // 
            // ClearLog
            // 
            this.ClearLog.Location = new System.Drawing.Point(395, 9);
            this.ClearLog.Name = "ClearLog";
            this.ClearLog.Size = new System.Drawing.Size(70, 30);
            this.ClearLog.TabIndex = 6;
            this.ClearLog.Text = "Clear";
            this.ClearLog.UseVisualStyleBackColor = true;
            this.ClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // MoraleToZero
            // 
            this.MoraleToZero.Location = new System.Drawing.Point(362, 110);
            this.MoraleToZero.Name = "MoraleToZero";
            this.MoraleToZero.Size = new System.Drawing.Size(103, 30);
            this.MoraleToZero.TabIndex = 7;
            this.MoraleToZero.Text = "All Morale to 0";
            this.MoraleToZero.UseVisualStyleBackColor = true;
            this.MoraleToZero.Click += new System.EventHandler(this.MoraleToZero_Click);
            // 
            // PlayerMoraleTo1
            // 
            this.PlayerMoraleTo1.Location = new System.Drawing.Point(362, 146);
            this.PlayerMoraleTo1.Name = "PlayerMoraleTo1";
            this.PlayerMoraleTo1.Size = new System.Drawing.Size(103, 30);
            this.PlayerMoraleTo1.TabIndex = 8;
            this.PlayerMoraleTo1.Text = "Player Morale = 1";
            this.PlayerMoraleTo1.UseVisualStyleBackColor = true;
            this.PlayerMoraleTo1.Click += new System.EventHandler(this.PlayerMoraleTo1_Click);
            // 
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(11, 48);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(311, 20);
            this.PathTextBox.TabIndex = 2;
            this.PathTextBox.Text = "S:\\Dropbox\\Paradox Interactive\\Crusader Kings II\\save games";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(328, 48);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(28, 20);
            this.browseButton.TabIndex = 3;
            this.browseButton.Text = "...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.BrowseClick);
            // 
            // ListFiles
            // 
            this.ListFiles.Location = new System.Drawing.Point(362, 74);
            this.ListFiles.Name = "ListFiles";
            this.ListFiles.Size = new System.Drawing.Size(103, 30);
            this.ListFiles.TabIndex = 9;
            this.ListFiles.Text = "Load Files";
            this.ListFiles.UseVisualStyleBackColor = true;
            this.ListFiles.Click += new System.EventHandler(this.ListFilesClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 535);
            this.Controls.Add(this.ListFiles);
            this.Controls.Add(this.PlayerMoraleTo1);
            this.Controls.Add(this.MoraleToZero);
            this.Controls.Add(this.ClearLog);
            this.Controls.Add(this.LogList);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.PathTextBox);
            this.Controls.Add(this.LabelAvailableFiles);
            this.Controls.Add(this.FilesListBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox FilesListBox;
        private System.Windows.Forms.Label LabelAvailableFiles;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.ListBox LogList;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.Button MoraleToZero;
        private System.Windows.Forms.Button PlayerMoraleTo1;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button ListFiles;
    }
}

