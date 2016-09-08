using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Ck2.Save;

namespace Ck2.Trainer
{
    public partial class FrmMain : Form
    {

        public static FrmMain Singleton;

        public FrmMain()
        {
            LoadedFileProcessors = new Dictionary<string, Type>();
            InitializeComponent();
            Singleton = this;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ResetFilePropertiesList();
            PopulateProcessorsList();
            SetDefaultPathForFileSearch();
            ListFilesInSelectedDir();
        }

        private void cbBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog.Description = "Choose root of savegames folder";
            FolderBrowserDialog.SelectedPath = tbSaveDir.Text;
            FolderBrowserDialog.ShowDialog();
            tbSaveDir.Text = FolderBrowserDialog.SelectedPath;

            FrmMain.AddLogEntry($"cd '{tbSaveDir.Text}'");
            int nbFilesFound = ListFilesInSelectedDir();
            FrmMain.AddLogEntry($" - Found {nbFilesFound} save files");

        }


        private void cbApplyProcessor_Click(object sender, EventArgs e)
        {
            try
            {
                if (ConfirmApplySelectedProcessor())
                    ApplySelectedProcessor();
            }
            catch (InvalidOperationException ioe)
            {
                MessageBox.Show(this, ioe.Message, "Error", MessageBoxButtons.OK);
            }
        }


        private void lbAvailableFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string fullPath = _selectedDir + @"\\" + lbAvailableFiles.SelectedItem.ToString();
            _selectedFile = new FileInfo(fullPath);
        }




        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogList.Items.Clear();
            LogList.Refresh();
        }



        /// <summary>
        /// Set the UI to accept or not user interaction, except Cancel
        /// </summary>
        /// <param name="acceptProcessingOrders"></param>
        private void SetUiEnable(bool acceptProcessingOrders)
        {
            cbClearFileList.Enabled = !acceptProcessingOrders;
            cbListFiles.Enabled = !acceptProcessingOrders;
            cbWriteToFile.Enabled = !acceptProcessingOrders;
            cbBrowse.Enabled = !acceptProcessingOrders;
            cbApplyProcessor.Enabled = !acceptProcessingOrders;
            cbClearLog.Enabled = !acceptProcessingOrders;

            cbCancel.Enabled = acceptProcessingOrders;
        }

        private CallerContext PrepareContextBeforeProcessing()
        {
            var cts = new CancellationTokenSource();
            var syncContext = SynchronizationContext.Current;
            Action<int> progressReport = (i => syncContext.Post(_ => ProgressBar.Increment(1), null));

            var context = new CallerContext()
            {
                CancelToken = cts.Token,
                ProgressReport = progressReport
            };

            _cancelAction = () =>
            {
                SetUiEnable(true);
                cts.Cancel();
            };

            ProgressBar.Value = 0;
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = Ck2SaveFile.EstimateNbLines(_selectedFile);
            return context;
        }





        private void cbLoadFiles_Click(object sender, EventArgs e)
        {
            LoadSelectedFileParallel();
        }

        private void cbListFiles_Click(object sender, EventArgs e)
        {
            ListFilesInSelectedDir();
        }

        private void cbClearFileList_Click(object sender, EventArgs e)
        {
            lbAvailableFiles.Items.Clear();
            lbAvailableFiles.Refresh();
        }

        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            _selectedDir = new DirectoryInfo(tbSaveDir.Text);
        }
    }
}
