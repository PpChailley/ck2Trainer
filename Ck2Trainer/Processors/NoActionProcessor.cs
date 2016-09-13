using Ck2.Save;
using Ck2.Save.File;
using Ck2.Save.Model;

namespace Ck2.Trainer.Processors
{
    public class NoActionProcessor: ICk2Processor
    {
        public FileChangeSet ApplyToNode(Mapping map)
        {
            return FileChangeSet.Empty;
        }
    }
}
