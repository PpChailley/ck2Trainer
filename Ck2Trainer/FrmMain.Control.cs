using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ck2.Save;
using Ck2.Trainer.Processors;

namespace Ck2.Trainer
{
    public partial class FrmMain
    {

        public const string COMMON_PATH = @"D:\Users\pipo\Dropbox";
        //public const string DEFAULT_SEARCH_PATH = COMMON_PATH + @"\Documents\Paradox Interactive\Crusader Kings II\save games";
        public const string DEFAULT_SEARCH_PATH = COMMON_PATH + @"\IsoFiling\Development\ck2Trainer\Data\readonly";


        private FileInfo _selectedFile;
        private DirectoryInfo _selectedDir;
        public Dictionary<string, Type> LoadedFileProcessors { get; }

        private SaveFile _loadedSaveFile;
        private Action _cancelAction;


        public SaveFile LoadedSaveFile
        {
            get
            {
                if (_loadedSaveFile == null)
                    throw new InvalidOperationException("No save file has been loaded");
                return _loadedSaveFile;
            }
        }




        private void ResetFilePropertiesList()
        {
            lbFileProperties.Items.Clear();
            lbFileProperties.Items.Add("File properties (Nothing loaded)");
        }

        private int ListFilesInSelectedDir()
        {
            int toreturn = ListFilesRecursive(tbSaveDir.Text);
            return toreturn;
        }

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


        private void LoadSelectedFileParallel()
        {
            SetUiEnable(false);

            try
            {
                var context = PrepareContextBeforeProcessing();
                var task = Task.Factory.StartNew(() =>
                {
                    Thread.CurrentThread.Name += " - Worker Load File";

                    _loadedSaveFile = new Ck2SaveFile(_selectedFile);
                    _loadedSaveFile.Parse(context);

                    context.CancelToken.ThrowIfCancellationRequested();
                },
                context.CancelToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
                );
            }
            finally
            {
                SetUiEnable(true);
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
            tbSaveDir.Text = DEFAULT_SEARCH_PATH;
        }
    }
}