using System;
using System.IO;

namespace Ck2.Save

{
    public class SaveFile
    {
        private FileInfo _file;
        private readonly StreamReader _fstream;


        public SaveFile(FileInfo f)
        {
            _file = f;
            _fstream = f.OpenText();
            CheckFileValidity();
        }

        public SaveFile(string s) : this(new FileInfo(s)) { }


        private void CheckFileValidity()
        {
            var openingLine = _fstream.ReadLine();
            var versionLine = _fstream.ReadLine();
            if (openingLine.Equals("CK2txt") == false)
            {
                throw new InvalidOperationException("File early consistency check fails. Refuse to open");
            }
            if (versionLine.Trim().Equals("version=\"2.5.2.0\"") == false)
            {
                throw new InvalidOperationException("File Version mismatch. Refuse to open");
            }

        }
    }
}
