using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public abstract class MappedBlock
    {
        protected DataBlock D;
        protected DataBlock Root;
        protected Mapping M;

        protected MappedBlock(DataBlock block, Mapping mapping)
        {
            D = block;
            Root = block.RootParent;
            M = mapping;
        }
    }
}