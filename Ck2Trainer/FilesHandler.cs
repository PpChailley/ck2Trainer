using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ck2.Save;

namespace Ck2.Trainer
{
    public class FilesHandler
    {
        public const string COMMON_PATH = @"D:\Users\pipo\Dropbox";
        //public const string DEFAULT_SEARCH_PATH = COMMON_PATH + @"\Documents\Paradox Interactive\Crusader Kings II\save games";
        public const string DEFAULT_SEARCH_PATH = COMMON_PATH + @"\IsoFiling\Development\ck2Trainer\Data\readonly";


        private readonly FrmMain _frmMain;

        internal FileInfo SelectedFile;
        internal DirectoryInfo SelectedDir;
        private SaveFile _loadedSaveFile;

        public FilesHandler(FrmMain frmMain)
        {
            _frmMain = frmMain;
        }

        internal int ListFilesInSelectedDir()
        {
            int toreturn = ListFilesRecursive(_frmMain.tbSaveDir.Text);
            return toreturn;
        }

        private int ListFilesRecursive(string path)
        {
            var files = Directory.GetFiles(path, "*.ck2");
            int found = files.Count();
            foreach (string file in files)
            {
                string relativePath = new Uri(path).MakeRelativeUri(new Uri(file)).ToString();
                string relativeString = relativePath.Substring(relativePath.IndexOf(@"/", StringComparison.Ordinal));
                _frmMain.lbAvailableFiles.Items.Add(Uri.UnescapeDataString(relativeString));

            }

            var subdirs = Directory.GetDirectories(path);
            found += subdirs.Sum(ListFilesRecursive);

            _frmMain.lbAvailableFiles.Refresh();

            return found;
        }

        public SaveFile LoadedSaveFile
        {
            get
            {
                if (_loadedSaveFile == null)
                    throw new InvalidOperationException("No save file has been loaded");
                return _loadedSaveFile;
            }
        }


        internal void LoadSelectedFileParallel()
        {
            _frmMain.SetUiEnable(false);

            try
            {
                var context = _frmMain.PrepareContextBeforeProcessing();
                var task = Task.Factory.StartNew(() =>
                {
                    Thread.CurrentThread.Name += " - Worker Load File";

                    _loadedSaveFile = new Ck2SaveFile(SelectedFile);
                    LoadedSaveFile.Parse(context);

                    context.CancelToken.ThrowIfCancellationRequested();
                },
                context.CancelToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default
                );
            }
            finally
            {
                _frmMain.SetUiEnable(true);
            }
        }
    }

}