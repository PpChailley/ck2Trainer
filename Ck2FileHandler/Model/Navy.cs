using System.Collections.Generic;
using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class Navy: MappedBlock
    {
        public Navy(DataBlock block, Mapping mapping) : base(block, mapping) { }

        private IEnumerable<Army> _armies;
        public IEnumerable<Army> Armies => _armies ?? (_armies = D.Blocks("army").Select(block => new Army(block, M)));
    }
}