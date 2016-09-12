using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class SubUnit: MappedBlock
    {
        public SubUnit(DataBlock block, Mapping mapping) : base(block, mapping) { }

        private Character _owner;
        public Character Owner => _owner?? (_owner = M.Characters.Single(c => c.Id == int.Parse(D.Value("owner"))));

        private int? _id;
        public int Id => _id?? (int) (_id = int.Parse(D.Block("id").Value("id")) as int?);

        public override string ToString()
        {
            return $"SubUnit [id={Id}]";
        }
    }
}