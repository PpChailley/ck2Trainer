using Ck2.Save;

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
