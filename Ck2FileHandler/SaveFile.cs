using System;
using System.IO;

namespace Ck2.Save

{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class SaveFile
    {
        internal FileInfo File;
        internal readonly StreamReader Stream;
        public static int NbReadLines;
        


        public SaveFile(FileInfo f)
        {
            File = f;
            Stream = f.OpenText();
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


        private void CheckFileValidity()
        {
            var openingLine = Stream.ReadLine();
            var versionLine = Stream.ReadLine();
            if (openingLine.Equals("CK2txt") == false)
            {
                throw new InvalidOperationException("File early consistency check fails. Refuse to open");
            }
            if (versionLine.Trim().Equals("version=\"2.5.2.0\"") == false)
            {
                throw new InvalidOperationException("File Version mismatch. Refuse to open");
            }

        }

        public TextBlock ReadTextBlocks()
        {
            var rootBlock = new TextBlock(null, this);
            return rootBlock;
        }
    }
}
