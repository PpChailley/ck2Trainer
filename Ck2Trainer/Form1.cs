using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ck2Trainer
{
    public partial class Form1 : Form
    {

        public static Form1 singleton;

        public Form1()
        {
            InitializeComponent();
            singleton = this;
        }

        private void BrowseClick(object sender, EventArgs e)
        {
            FolderBrowserDialog.Description = "Choose root of savegames folder";
            FolderBrowserDialog.SelectedPath = PathTextBox.Text;
            FolderBrowserDialog.ShowDialog();
            PathTextBox.Text = FolderBrowserDialog.SelectedPath;

            Form1.AddLogEntry(String.Format("cd '{0}'", PathTextBox.Text));
            var nbFilesFound = ListFilesInSelectedDir(PathTextBox.Text);
            Form1.AddLogEntry(String.Format(" - Found {0} save files", nbFilesFound));


        }

        private int ListFilesInSelectedDir(string path)
        {
            int toreturn = ListFilesRecursive(path);
            FilesListBox.Refresh();

            return toreturn;

        }

        public static void AddLogEntry(string text)
        {
            singleton.LogList.Items.Add(text);
            singleton.LogList.Refresh();
        }

        private int ListFilesRecursive(string path)
        {
            var files = Directory.GetFiles(path, "*.ck2");
            int found = files.Count();
            foreach (var file in files)
            {
                FilesListBox.Items.Add(file);
            }

            var subdirs = Directory.GetDirectories(path);
            found += subdirs.Sum(dir => ListFilesRecursive(dir));

            return found;
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            LogList.Items.Clear();
            LogList.Refresh();
        }

        private void MoraleToZero_Click(object sender, EventArgs e)
        {
            ProcessFiles(new[] {new MoraleToZeroFileProcessor()});
        }

        private void ProcessFiles(ICollection<IFileProcessor> processors)
        {
            StreamReader input = OpenSelectedFileAsInputReader();
            StreamWriter output = OpenTargetFileAsOutputReader();

            int nbLines = 0;

            try
            {
                while (!input.EndOfStream)
                {
                    var line = input.ReadLine();

                    if (line.Contains("lastmonthexpensetable"))
                    {
                        int i = 0;
                    }





                    foreach (var fileProcessor in processors)
                    {
                        line = fileProcessor.Process(line);
                    }
                    output.WriteLine(line);
                    output.Flush();
                    nbLines++;
                }
            }
            finally
            {
                input.Close();
                output.Flush();
                output.Close();
            }

            this.LogList.Items.Add(String.Format("Done. {0} lines read", nbLines));

        }

        private StreamWriter OpenTargetFileAsOutputReader()
        {
            string src = FilesListBox.SelectedItem.ToString();
            string dest = src + ".hacked." + DateTime.Now.ToShortTimeString().Replace(":", ".") + ".ck2";

            StreamWriter writer = new StreamWriter(dest);

            return writer;
        }

        private StreamReader OpenSelectedFileAsInputReader()
        {
            string filename = FilesListBox.SelectedItem.ToString();
            StreamReader reader = new StreamReader(filename, Encoding.Default);

            return reader;
        }

        private void PlayerMoraleTo1_Click(object sender, EventArgs e)
        {
            ProcessFiles(new[] { new PlayerMoraleToOneFileProcessor() });
        }

        private void ListFilesClick(object sender, EventArgs e)
        {
            ListFilesRecursive(PathTextBox.Text);
        }
    }
}
