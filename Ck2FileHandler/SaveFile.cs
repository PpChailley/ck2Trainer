using System;
using System.Collections.Generic;
using System.IO;
using ck2.Mapping.Save.Extensions;

namespace Ck2.Save

{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class SaveFile
    {
        private readonly FileInfo _file;
        private readonly StreamReader _stream;
        public bool EndOfStream => _stream.EndOfStream;

        public bool FullyParsed = false;


        public int NbReadLines { get; private set; }


        private DataBlock _rootBlock;
        public DataBlock RootBlock
        {
            get
            {
                if (_rootBlock == null)
                    throw new InvalidOperationException("Cannot access data objects before parsing");

                return _rootBlock;
            }
            private set { _rootBlock = value; }
        }


        public SaveFile(string s) : this(new FileInfo(s)) { }

        public SaveFile(FileInfo f)
        {
            _file = f;
            _stream = f.OpenText();
            //CheckFileValidity();
        }


        private string ReadLine()
        {
            var s = _stream.ReadLine();
            NbReadLines ++;
            return s;
        }

        private void CheckFileValidity()
        {
            var fileHeader = RootBlock.Children[0];
            var fileVersion = RootBlock.Children[1];

            if ( fileHeader is DataLine == false
                || ((DataLine)fileHeader).AsText.Equals("CK2txt") == false)
            { 
                throw new InvalidOperationException("File early consistency check fails. Refuse to open");
            }

            if (fileVersion is DataLine == false
                || ((DataLine)fileVersion).AsText.Equals("version=\"2.5.2.0\"") == false)

            {
                throw new InvalidOperationException("File Version mismatch. Refuse to open");
            }
        }

        public void Parse(CallerContext context)
        {
            FullyParsed = false;

            RootBlock = new DataBlock(null);
            IDataElement target = RootBlock;

            if (ReportProgress(context)) return;

            while (_stream.EndOfStream == false)
            {
                if (ReportProgress(context)) return;

                var line = ReadLine();
                var splitLine = SplitLine(line);

                foreach (var subLine in splitLine)
                {
                     target = target.ProcessLine(subLine);
                }

                if (NbReadLines == 5 || _stream.EndOfStream)
                {
                    CheckFileValidity();
                }
            }

            if (RootBlock.Children.Count == 0)
            {
                throw new InvalidOperationException("Could not read any data. Possibly empty file");
            }

            FullyParsed = true;
        }

        private bool ReportProgress(CallerContext context)
        {
            if (context.Cancel.IsCancellationRequested)
            {
                RemoveParsedData();
                return true;
            }

            context.Progress.Report(NbReadLines);
            return false;
        }

        private void RemoveParsedData()
        {
            RootBlock = null;
            FullyParsed = false;
            GC.Collect();
        }


        private IEnumerable<string> SplitLine(string line)
        {
            return line.SplitAndKeep(new[] { '{', '}' });
        }



        public void WriteTo(string destFileName)
        {
            using (StreamWriter outputFile = new StreamWriter(destFileName))
            {
                foreach (var element in RootBlock.Children)
                {
                    var toWrite = element.ToIndentedString();
                    outputFile.WriteLine(toWrite);
                }
            }

        }
    }
}
