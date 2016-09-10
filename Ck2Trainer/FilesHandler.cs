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
        private SaveFile _f;

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

        public SaveFile F
        {
            get
            {
                if (_f == null)
                    throw new InvalidOperationException("No save file has been loaded");
                return _f;
            }
        }


        internal void LoadSelectedFileParallel()
        {
            _frmMain.SetUiEnable(false);

            try
            {
                var context = _frmMain.PrepareContextBeforeProcessing();
                var task = Task.Factory.StartNew( () =>
                {
                    Thread.CurrentThread.Name += " - Worker Load File";

                    _f = new Ck2SaveFile(SelectedFile);
                    F.Parse(context);

                    context.CancelToken.ThrowIfCancellationRequested();
                },
                context.CancelToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);

                task.ContinueWith( _ =>
                {
                    try
                    {
                        //task.Wait();
                    }
                    finally
                    {
                        if (_frmMain.InvokeRequired && _frmMain.IsHandleCreated)
                        {
                            _frmMain.Invoke((Action) (() => _frmMain.SetUiEnable(true)));
                            _frmMain.Invoke((Action) (() => _frmMain.DisplaySaveAbstract(_f.Abstract)));
                        }
                        else
                        {
                            _frmMain.SetUiEnable(true);
                            _frmMain.DisplaySaveAbstract(_f.Abstract);
                        }

                        if (_f.FullyParsed == false)
                            throw new InvalidOperationException("File load is complete but parsing did not");
                    }
                } , 
                context.CancelToken,
                TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}