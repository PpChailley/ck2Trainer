using System.Collections.Generic;
using System.Linq;

namespace Ck2.Save.Model
{
    public class Army : MappedBlock
    {
        public Army(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public int Id => int.Parse(D.Block("id").Value("id"));

        public IEnumerable<SubUnit> SubUnits => D.Blocks("sub_unit").Select( b => new SubUnit(b, M));
    }
}