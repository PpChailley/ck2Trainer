using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ck2.Save;

namespace Ck2.Trainer.Processors
{
    public class NoActionProcessor: ICk2Processor
    {

        FileChangeSet ICk2Processor.ApplyToNode(DataBlock node)
        {
            throw new NotImplementedException();
        }
    }
}
