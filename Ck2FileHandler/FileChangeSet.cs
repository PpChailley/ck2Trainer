using System.Collections.Generic;

namespace Ck2.Save
{
    public class FileChangeSet: List<FileChange>, IList<FileChange>
    {

        private FileChangeSet(int capacity) : base(capacity) { }



        public static FileChangeSet Empty => new FileChangeSet(0);


    }
}