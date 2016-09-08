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
            this.cbClearLog = new System.Windows.Forms.Button();
            this.tbSaveDir = new System.Windows.Forms.TextBox();
            this.cbBrowse = new System.Windows.Forms.Button();
            this.FolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.cbLoadFile = new System.Windows.Forms.Button();
            this.cbWriteToFile = new System.Windows.Forms.Button();
            this.lbFileProperties = new System.Windows.Forms.ListBox();
            this.gbAvailableFiles = new System.Windows.Forms.GroupBox();
            this.cbListFiles = new System.Windows.Forms.Button();
            this.cbClearFileList = new System.Windows.Forms.Button();
            this.lbAvailableFiles = new System.Windows.Forms.ListBox();
            this.cbApplyProcessor = new System.Windows.Forms.Button();
            this.cbCancel = new System.Windows.Forms.Button();
            this.gbAvailableFiles.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProcessors
            // 
            this.lbProcessors.FormattingEnabled = true;
            this.lbProcessors.HorizontalScrollbar = true;
            this.lbProcessors.Location = new System.Drawing.Point(12, 308);
            this.lbProcessors.Name = "lbProcessors";
            this.lbProcessors.Size = new System.Drawing.Size(345, 186);
            this.lbProcessors.TabIndex = 0;
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(87, 500);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(838, 20);
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
            // cbClearLog
            // 
            this.cbClearLog.Location = new System.Drawing.Point(395, 451);
            this.cbClearLog.Name = "cbClearLog";
            this.cbClearLog.Size = new System.Drawing.Size(70, 30);
            this.cbClearLog.TabIndex = 6;
            this.cbClearLog.Text = "Clear";
            this.cbClearLog.UseVisualStyleBackColor = true;
            this.cbClearLog.Click += new System.EventHandler(this.ClearLog_Click);
            // 
            // tbSaveDir
            // 
            this.tbSaveDir.Location = new System.Drawing.Point(8, 19);
            this.tbSaveDir.Name = "tbSaveDir";
            this.tbSaveDir.Size = new System.Drawing.Size(296, 20);
            this.tbSaveDir.TabIndex = 2;
            this.tbSaveDir.Text = "S:\\Dropbox\\Paradox Interactive\\Crusader Kings II\\save games";
            this.tbSaveDir.TextChanged += new System.EventHandler(this.PathTextBox_TextChanged);
            // 
            // cbBrowse
            // 
            this.cbBrowse.Location = new System.Drawing.Point(310, 20);
            this.cbBrowse.Name = "cbBrowse";
            this.cbBrowse.Size = new System.Drawing.Size(28, 20);
            this.cbBrowse.TabIndex = 3;
            this.cbBrowse.Text = "...";
            this.cbBrowse.UseVisualStyleBackColor = true;
            this.cbBrowse.Click += new System.EventHandler(this.cbBrowse_Click);
            // 
            // cbLoadFile
            // 
            this.cbLoadFile.Location = new System.Drawing.Point(351, 97);
            this.cbLoadFile.Name = "cbLoadFile";
            this.cbLoadFile.Size = new System.Drawing.Size(80, 30);
            this.cbLoadFile.TabIndex = 9;
            this.cbLoadFile.Text = "Load File";
            this.cbLoadFile.UseVisualStyleBackColor = true;
            this.cbLoadFile.Click += new System.EventHandler(cbLoadFiles_Click);
            // 
            // cbWriteToFile
            // 
            this.cbWriteToFile.Location = new System.Drawing.Point(363, 358);
            this.cbWriteToFile.Name = "cbWriteToFile";
            this.cbWriteToFile.Size = new System.Drawing.Size(80, 30);
            this.cbWriteToFile.TabIndex = 10;
            this.cbWriteToFile.Text = "-- WRITE --";
            this.cbWriteToFile.UseVisualStyleBackColor = true;
            // 
            // lbFileProperties
            // 
            this.lbFileProperties.FormattingEnabled = true;
            this.lbFileProperties.HorizontalScrollbar = true;
            this.lbFileProperties.Location = new System.Drawing.Point(12, 152);
            this.lbFileProperties.Name = "lbFileProperties";
            this.lbFileProperties.Size = new System.Drawing.Size(453, 108);
            this.lbFileProperties.TabIndex = 11;
            // 
            // gbAvailableFiles
            // 
            this.gbAvailableFiles.Controls.Add(this.cbListFiles);
            this.gbAvailableFiles.Controls.Add(this.cbClearFileList);
            this.gbAvailableFiles.Controls.Add(this.lbAvailableFiles);
            this.gbAvailableFiles.Controls.Add(this.tbSaveDir);
            this.gbAvailableFiles.Controls.Add(this.cbBrowse);
            this.gbAvailableFiles.Controls.Add(this.cbLoadFile);
            this.gbAvailableFiles.Location = new System.Drawing.Point(11, 12);
            this.gbAvailableFiles.Name = "gbAvailableFiles";
            this.gbAvailableFiles.Size = new System.Drawing.Size(454, 134);
            this.gbAvailableFiles.TabIndex = 12;
            this.gbAvailableFiles.TabStop = false;
            this.gbAvailableFiles.Text = "Save Directory";
            // 
            // cbListFiles
            // 
            this.cbListFiles.Location = new System.Drawing.Point(352, 59);
            this.cbListFiles.Name = "cbListFiles";
            this.cbListFiles.Size = new System.Drawing.Size(80, 30);
            this.cbListFiles.TabIndex = 14;
            this.cbListFiles.Text = "List";
            this.cbListFiles.UseVisualStyleBackColor = true;
            this.cbListFiles.Click += new System.EventHandler(this.cbListFiles_Click);
            // 
            // cbClearFileList
            // 
            this.cbClearFileList.Location = new System.Drawing.Point(351, 23);
            this.cbClearFileList.Name = "cbClearFileList";
            this.cbClearFileList.Size = new System.Drawing.Size(80, 30);
            this.cbClearFileList.TabIndex = 13;
            this.cbClearFileList.Text = "Clear";
            this.cbClearFileList.UseVisualStyleBackColor = true;
            this.cbClearFileList.Click += new System.EventHandler(this.cbClearFileList_Click);
            // 
            // lbAvailableFiles
            // 
            this.lbAvailableFiles.FormattingEnabled = true;
            this.lbAvailableFiles.HorizontalScrollbar = true;
            this.lbAvailableFiles.Location = new System.Drawing.Point(6, 45);
            this.lbAvailableFiles.Name = "lbAvailableFiles";
            this.lbAvailableFiles.Size = new System.Drawing.Size(330, 82);
            this.lbAvailableFiles.TabIndex = 12;
            this.lbAvailableFiles.SelectedIndexChanged += new System.EventHandler(this.lbAvailableFiles_SelectedIndexChanged);
            // 
            // cbApplyProcessor
            // 
            this.cbApplyProcessor.Location = new System.Drawing.Point(362, 308);
            this.cbApplyProcessor.Name = "cbApplyProcessor";
            this.cbApplyProcessor.Size = new System.Drawing.Size(103, 30);
            this.cbApplyProcessor.TabIndex = 13;
            this.cbApplyProcessor.Text = " --- Apply --->";
            this.cbApplyProcessor.UseVisualStyleBackColor = true;
            this.cbApplyProcessor.Click += new System.EventHandler(this.cbApplyProcessor_Click);
            // 
            // cbCancel
            // 
            this.cbCancel.Location = new System.Drawing.Point(11, 500);
            this.cbCancel.Name = "cbCancel";
            this.cbCancel.Size = new System.Drawing.Size(70, 20);
            this.cbCancel.TabIndex = 14;
            this.cbCancel.Text = "Cancel";
            this.cbCancel.UseVisualStyleBackColor = true;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 535);
            this.Controls.Add(this.cbCancel);
            this.Controls.Add(this.cbApplyProcessor);
            this.Controls.Add(this.lbFileProperties);
            this.Controls.Add(this.gbAvailableFiles);
            this.Controls.Add(this.cbClearLog);
            this.Controls.Add(this.cbWriteToFile);
            this.Controls.Add(this.LogList);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.lbProcessors);
            this.Name = "FrmMain";
            this.Text = "(will be changed at form load)";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gbAvailableFiles.ResumeLayout(false);
            this.gbAvailableFiles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbProcessors;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.ListBox LogList;
        private System.Windows.Forms.Button cbClearLog;
        private System.Windows.Forms.TextBox tbSaveDir;
        private System.Windows.Forms.Button cbBrowse;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog;
        private System.Windows.Forms.Button cbLoadFile;
        private System.Windows.Forms.Button cbWriteToFile;
        private System.Windows.Forms.ListBox lbFileProperties;
        private System.Windows.Forms.GroupBox gbAvailableFiles;
        private System.Windows.Forms.Button cbApplyProcessor;
        private System.Windows.Forms.ListBox lbAvailableFiles;
        private System.Windows.Forms.Button cbListFiles;
        private System.Windows.Forms.Button cbClearFileList;
        private System.Windows.Forms.Button cbCancel;
    }
}

