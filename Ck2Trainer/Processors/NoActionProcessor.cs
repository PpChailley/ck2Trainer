using Ck2.Save;
using Ck2.Save.File;

namespace Ck2.Trainer.Processors
{
    public class NoActionProcessor: ICk2Processor
    {

        FileChangeSet ICk2Processor.ApplyToNode(DataBlock node)
        {
            return FileChangeSet.Empty;
        }
    }
}
