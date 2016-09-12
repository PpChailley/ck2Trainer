using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class Dynasty: MappedBlock
    {
        public Dynasty(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public Property Name => D.Property("name");
    }
}