using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class SubUnit: MappedBlock
    {
        public SubUnit(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public Character Owner => M.Characters.Single(c => c.Id == int.Parse(D.Value("owner")));

    }
}