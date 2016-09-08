using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ck2.Trainer
{
    public partial class FrmMain : Form
    {

        #region internals

        public static FrmMain Singleton;

        public FrmMain()
        {
            InitializeComponent();
            Singleton = this;
        }

        #endregion

        #region events

        private void BrowseClick(object sender, EventArgs e)
        {
            FolderBrowserDialog.Description = "Choose root of savegames folder";
            FolderBrowserDialog.SelectedPath = PathTextBox.Text;
            FolderBrowserDialog.ShowDialog();
            PathTextBox.Text = FolderBrowserDialog.SelectedPath;

            FrmMain.AddLogEntry($"cd '{PathTextBox.Text}'");
            var nbFilesFound = ListFilesInSelectedDir(PathTextBox.Text);
            FrmMain.AddLogEntry($" - Found {nbFilesFound} save files");


        }

        private int ListFilesInSelectedDir(string path)
        {
            int toreturn = ListFilesRecursive(path);
            lbProcessors.Refresh();

            return toreturn;

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
            foreach (var file in files)
            {
                lbProcessors.Items.Add(file);
            }

            var subdirs = Directory.GetDirectories(path);
            found += subdirs.Sum(ListFilesRecursive);

            return found;
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogList.Items.Clear();
            LogList.Refresh();
        }


        private StreamWriter OpenTargetFileAsOutputReader()
        {
            string src = lbProcessors.SelectedItem.ToString();
            string dest = src + ".hacked." + DateTime.Now.ToShortTimeString().Replace(":", ".") + ".ck2";

            StreamWriter writer = new StreamWriter(dest);

            return writer;
        }

        private StreamReader OpenSelectedFileAsInputReader()
        {
            string filename = lbProcessors.SelectedItem.ToString();
            StreamReader reader = new StreamReader(filename, Encoding.Default);

            return reader;
        }

  

        private void ListFilesClick(object sender, EventArgs e)
        {
            ListFilesRecursive(PathTextBox.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lbFileProperties.Items.Clear();
            lbFileProperties.Items.Add("File properties (Nothing loaded)");
        }

        private void lbFileProperties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
