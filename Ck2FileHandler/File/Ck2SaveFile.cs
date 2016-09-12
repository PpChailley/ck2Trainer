using System.IO;

namespace Ck2.Save.File

{
    public class Ck2SaveFile : SaveFile
    {

        private const float AVG_CHAR_PER_LINE = 17.4F;


        public Ck2SaveFile(string s) : base(s) { }
        public Ck2SaveFile(FileInfo f) : base(f) { }



        public static int EstimateNbLines(FileInfo f)
        {
            return (int)(f.Length / AVG_CHAR_PER_LINE);
        }



    }

}