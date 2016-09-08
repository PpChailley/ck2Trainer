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
        public FilesHandler FilesHandler { get; }
        public ProcessorsHandler ProcessorsHandler { get; }


        private Action _cancelAction;



        private void ResetFilePropertiesList()
        {
            lbFileProperties.Items.Clear();
            lbFileProperties.Items.Add("File properties (Nothing loaded)");
        }


        public static void AddLogEntry(string text)
        {
            Singleton.LogList.Items.Add(text);
            Singleton.LogList.Refresh();
        }



        private void SetDefaultPathForFileSearch()
        {
            tbSaveDir.Text = FilesHandler.DEFAULT_SEARCH_PATH;
        }
    }
}