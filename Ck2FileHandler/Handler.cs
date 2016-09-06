using System;
using System.IO;

namespace Ck2.FileHandler

{
    public class Handler
    {
        private FileInfo _file;
        private StreamReader _fstream;


        public Handler(FileInfo f)
        {
            _file = f;
            _fstream = f.OpenText();
            CheckFileValidity();
        }




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
