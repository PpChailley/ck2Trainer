namespace Ck2.Trainer
{
    partial class FrmMain
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
            this.lbProcessors = new System.Windows.Forms.ListBox();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.LogList = new System.Windows.Forms.ListBox();
            this.ClearLog = new System.Windows.Forms.Button();
            this.PathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.ListFiles = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lbFileProperties = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProcessors
            // 
            this.lbProcessors.FormattingEnabled = true;
            this.lbProcessors.HorizontalScrollbar = true;
            this.lbProcessors.Location = new System.Drawing.Point(12, 165);
            this.lbProcessors.Name = "lbProcessors";
            this.lbProcessors.Size = new System.Drawing.Size(345, 329);
            this.lbProcessors.TabIndex = 0;
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
            // PathTextBox
            // 
            this.PathTextBox.Location = new System.Drawing.Point(8, 19);
            this.PathTextBox.Name = "PathTextBox";
            this.PathTextBox.Size = new System.Drawing.Size(296, 20);
            this.PathTextBox.TabIndex = 2;
            this.PathTextBox.Text = "S:\\Dropbox\\Paradox Interactive\\Crusader Kings II\\save games";
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(310, 20);
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
            this.ListFiles.Text = "Load File";
            this.ListFiles.UseVisualStyleBackColor = true;
            this.ListFiles.Click += new System.EventHandler(this.ListFilesClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(362, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 30);
            this.button1.TabIndex = 10;
            this.button1.Text = "-- WRITE --";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // lbFileProperties
            // 
            this.lbFileProperties.FormattingEnabled = true;
            this.lbFileProperties.HorizontalScrollbar = true;
            this.lbFileProperties.Location = new System.Drawing.Point(8, 45);
            this.lbFileProperties.Name = "lbFileProperties";
            this.lbFileProperties.Size = new System.Drawing.Size(330, 82);
            this.lbFileProperties.TabIndex = 11;
            this.lbFileProperties.SelectedIndexChanged += new System.EventHandler(this.lbFileProperties_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PathTextBox);
            this.groupBox1.Controls.Add(this.lbFileProperties);
            this.groupBox1.Controls.Add(this.browseButton);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(344, 134);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input File";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 535);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ListFiles);
            this.Controls.Add(this.ClearLog);
            this.Controls.Add(this.LogList);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.lbProcessors);
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbProcessors;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.ListBox LogList;
        private System.Windows.Forms.Button ClearLog;
        private System.Windows.Forms.TextBox PathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button ListFiles;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox lbFileProperties;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

