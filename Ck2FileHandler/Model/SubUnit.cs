using System;
using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    [Obsolete]
    public class SubUnit: MappedBlock
    {
        public SubUnit(DataBlock block, Mapping mapping) : base(block, mapping) { }


        private int? _id;
        public int Id => _id?? (int) (_id = int.Parse(D.Block("id").Value("id")) as int?);


        public override string ToString()
        {
            return $"SubUnit [id={Id}]";
        }
    }
}