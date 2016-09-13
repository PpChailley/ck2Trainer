using System;
using Ck2.Save;
using Ck2.Save.File;
using Ck2.Save.Model;
using Ck2.Trainer.Processors;

namespace Ck2.Trainer.Test
{
    public class AllMoraleToZeroProcessor: ICk2Processor
    {
        public FileChangeSet ApplyToNode(Mapping map)
        {

            throw new NotImplementedException();
            /*
            var changes = new FileChangeSet();

            foreach (var army in map.AllArmies)
            {
                var before = army.Copy();
                army.Morale = 0;

                changes.Add(new FileChange()
                {
                    Before = before,
                    After = army
                });
            }

    // */
        }
    }
}