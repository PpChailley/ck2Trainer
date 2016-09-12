using System.Collections.Generic;
using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class Army : MappedBlock
    {
        public Army(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public int? Id
        {
            get
            {
                var foundId = D.Block("id");
                if (foundId == null)
                    return null;
                else
                    return int.Parse(foundId.Value("id"));
            }
        }

        public IEnumerable<SubUnit> SubUnits => D.Blocks("sub_unit").Select( b => new SubUnit(b, M));

    }
}