using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using ck2.Mapping.Save.Model;

namespace Ck2.Save

{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class SaveFile
    {
        private readonly FileInfo _file;
        private readonly StreamReader _stream;
        public int NbReadLines { get; private set; }
        public bool EndOfStream => _stream.EndOfStream;
        public DataBlock RootBlock { get; private set; }



        public SaveFile(string s) : this(new FileInfo(s)) { }
        public SaveFile(FileInfo f)
        {
            _file = f;
            _stream = f.OpenText();
            CheckFileValidity();
        }



        private int? _playerId;
        public int? PlayerId => _playerId ?? (_playerId = ReadPlayerId());

        private Player _player;
        public Player Player => _player ?? (_player = ReadPlayer());




        private Player ReadPlayer()
        {
            if (RootBlock == null)
            {
                throw new InvalidOperationException("Cannot read any info while file has not been parsed");
            }

            throw new NotImplementedException();
        }


        private int? ReadPlayerId()
        {
            if (RootBlock == null)
            {
                throw new InvalidOperationException("Cannot read any info while file has not been parsed");
            }

            throw new NotImplementedException();
        }



        private string ReadLine()
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

        public void Parse()
        {
            RootBlock = new DataBlock(null);
            IDataElement target = RootBlock;


            while (_stream.EndOfStream == false)
            {
                var line = ReadLine();
                var splitLine = SplitLine(line);

                foreach (var subLine in splitLine)
                {
                     target = target.ProcessLine(subLine);
                }
            }
        }


        private IList<string> SplitLine(string line)
        {
            List<string> l = new List<string>();
            var regex = new Regex(@"(.*?)([\{\}])(.*)");
            var matches = regex.Matches(line);

            while (matches.Count > 0)
            {
                l.Add(matches[0].Groups[1].Value);
                l.Add(matches[0].Groups[2].Value);
                line = matches[0].Groups[3].Value;
                matches = regex.Matches(line);
            }

            l.Add(line);
            return l;
        }
    }
}
