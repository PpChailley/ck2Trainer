using Ck2.Save.File;

namespace Ck2.Save.Model
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class Dynasty: MappedBlock
    {
        public Dynasty(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public Property Name => D.Property("name");
        public int Id => int.Parse(D.Name);
    }
}