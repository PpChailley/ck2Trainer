using Ck2.Save;
using Ck2.Save.File;
using Ck2.Save.Model;

namespace Ck2.Trainer.Processors
{
    public interface ICk2Processor
    {
        FileChangeSet ApplyToNode(Mapping map);
    }
}