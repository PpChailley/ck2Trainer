using System;
using System.IO;
using Ck2.Save.Model;

namespace Ck2.Save
{
    public class Ck2SaveFile : SaveFile
    {

        private const float AVG_CHAR_PER_LINE = 17.4F;


        public Ck2SaveFile(string s) : base(s) { }
        public Ck2SaveFile(FileInfo f) : base(f) { }


        private int? _playerId;
        private Player _player;


        public int? PlayerId => _playerId ?? (_playerId = ReadPlayerId());
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

        public static int EstimateNbLines(FileInfo f)
        {
            return (int)(f.Length / AVG_CHAR_PER_LINE);
        }



    }

}