using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Ck2.Save;
using Ck2.Trainer.Processors;

namespace Ck2.Trainer
{
    public partial class FrmMain : Form
    {
        public const string DEFAULT_SEARCH_PATH = @"D:\Users\pipo\Dropbox\Documents\Paradox Interactive\Crusader Kings II\save games";

        #region internals

        public static FrmMain Singleton;

        public Dictionary<string, Type> LoadedFileProcessors { get; }


        private SaveFile _loadedSaveFile;


        public SaveFile LoadedSaveFile
        {
            get
            {
                if (_loadedSaveFile == null)
                    throw new InvalidOperationException("No save file has been loaded");
                return _loadedSaveFile;
            }
        }




        public FrmMain()
        {
            LoadedFileProcessors = new Dictionary<string, Type>();
            InitializeComponent();
            Singleton = this;
        }

        #endregion

        #region events


        private void Form1_Load(object sender, EventArgs e)
        {
            ResetFilePropertiesList();
            PopulateProcessorsList();
            SetDefaultPathForFileSearch();
            ListFilesInSelectedDir();
        }



        private void ResetFilePropertiesList()
        {
            lbFileProperties.Items.Clear();
            lbFileProperties.Items.Add("File properties (Nothing loaded)");
        }


        private void BrowseClick(object sender, EventArgs e)
        {
            FolderBrowserDialog.Description = "Choose root of savegames folder";
            FolderBrowserDialog.SelectedPath = PathTextBox.Text;
            FolderBrowserDialog.ShowDialog();
            PathTextBox.Text = FolderBrowserDialog.SelectedPath;

            FrmMain.AddLogEntry($"cd '{PathTextBox.Text}'");
            int nbFilesFound = ListFilesInSelectedDir();
            FrmMain.AddLogEntry($" - Found {nbFilesFound} save files");

        }

        private int ListFilesInSelectedDir()
        {
            int toreturn = ListFilesRecursive(PathTextBox.Text);
            return toreturn;
        }

        private void btnApplyProcessor_Click(object sender, EventArgs e)
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


        #endregion

        public static void AddLogEntry(string text)
        {
            Singleton.LogList.Items.Add(text);
            Singleton.LogList.Refresh();
        }

        private int ListFilesRecursive(string path)
        {
            var files = Directory.GetFiles(path, "*.ck2");
            int found = files.Count();
            foreach (string file in files)
            {
                string relativePath = new Uri(path).MakeRelativeUri(new Uri(file)).ToString();
                string relativeString = relativePath.Substring(relativePath.IndexOf(@"/", StringComparison.Ordinal));
                lbAvailableFiles.Items.Add(Uri.UnescapeDataString(relativeString));
                
            }

            var subdirs = Directory.GetDirectories(path);
            found += subdirs.Sum(ListFilesRecursive);

            lbAvailableFiles.Refresh();

            return found;
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogList.Items.Clear();
            LogList.Refresh();
        }


        private void ListFilesClick(object sender, EventArgs e)
        {
            try
            {
                LoadSelectedFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, ex.GetType().Name, MessageBoxButtons.OK);
            }
        }

        private void LoadSelectedFile()
        {
            _loadedSaveFile = new Ck2SaveFile(SelectedFile);

            //TODO: Background this
            _loadedSaveFile.Parse();
        }

        public FileInfo SelectedFile
        {
            get
            {
                if (lbAvailableFiles.SelectedItem == null)
                    throw new InvalidOperationException("No file selected");

                return new FileInfo(PathTextBox.Text + lbAvailableFiles.SelectedItem);
            }

        }

        private void PopulateProcessorsList()
        {
            var processors = Assembly.GetExecutingAssembly().DefinedTypes
                .Where(t => t.ImplementedInterfaces.Contains(typeof(ICk2Processor)));

            foreach (var p in processors)
            {
                LoadedFileProcessors.Add(p.Name, p);
                lbProcessors.Items.Add(p.Name);
            }

            lbProcessors.Refresh();
        }

        private void ApplySelectedProcessor()
        {
            ((ICk2Processor)Activator.CreateInstance(SelectedProcessor))
                .ApplyToNode(LoadedSaveFile.RootBlock);
        }


        private bool ConfirmApplySelectedProcessor()
        {
            var answer = MessageBox.Show(this, 
                $"Confirm applying {SelectedProcessor.Name} processor ?", "Confirm",
                MessageBoxButtons.OKCancel);

            return answer == DialogResult.OK;
        }

        public Type SelectedProcessor
        {
            get
            {
                Type selected;
                if (lbFileProperties != null 
                    && LoadedFileProcessors.TryGetValue(lbProcessors.SelectedItem as string, out selected))
                {
                    return selected;
                }

                throw new InvalidOperationException();
            }
        }

        private void SetDefaultPathForFileSearch()
        {
            PathTextBox.Text = DEFAULT_SEARCH_PATH;
        }



    }
}
