using System;
using System.IO;

namespace Ck2.Save

{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class SaveFile
    {
        private readonly FileInfo _file;
        private readonly StreamReader _stream;
        public int NbReadLines { get; private set; }
        


        public SaveFile(FileInfo f)
        {
            _file = f;
            _stream = f.OpenText();
            CheckFileValidity();
        }

        public SaveFile(string s) : this(new FileInfo(s)) { }

        public int PlayerId
        {
            get
            {
                throw new NotImplementedException();
            } 
        }

        public object Player
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool EndOfStream => _stream.EndOfStream;

        public string ReadLine()
        {
            var s = _stream.ReadLine();
            NbReadLines ++;
            return s;
        }


        private void CheckFileValidity()
        {
            var s = _file.OpenText();

            try
            {
                var openingLine = s.ReadLine();
                var versionLine = s.ReadLine();
                if (openingLine.Equals("CK2txt") == false)
                {
                    throw new InvalidOperationException("File early consistency check fails. Refuse to open");
                }
                if (versionLine.Trim().Equals("version=\"2.5.2.0\"") == false)
                {
                    throw new InvalidOperationException("File Version mismatch. Refuse to open");
                }
            }
            finally
            {
                s.Close();
            }
        }

        public TextBlock ReadTextBlocks()
        {
            var rootBlock = new TextBlock(null, this);
            return rootBlock;
        }

    }
}
