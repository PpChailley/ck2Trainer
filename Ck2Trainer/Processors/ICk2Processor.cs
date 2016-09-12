using Ck2.Save;
using Ck2.Save.File;

namespace Ck2.Trainer.Processors
{
    public interface ICk2Processor
    {
        FileChangeSet ApplyToNode(DataBlock node);
    }
}