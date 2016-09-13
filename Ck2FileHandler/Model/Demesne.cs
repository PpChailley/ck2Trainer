using System.Collections.Generic;
using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class Demesne : MappedBlock
    {
        public Demesne(DataBlock block, Mapping mapping) : base(block, mapping) {}

        private IEnumerable<Army> _armies;
        public IEnumerable<Army> Armies => _armies ?? (_armies = D.Blocks("army").Select(block => new Army(block, M)));

        private IEnumerable<Navy> _navies;
        public IEnumerable<Navy> Navies => _navies ?? (_navies = D.Blocks("navy").Select(block => new Navy(block, M)));

    }
}