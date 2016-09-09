namespace Ck2.Save.Model
{
    public abstract class MappedBlock
    {
        protected DataBlock D;
        protected DataBlock Root;

        protected MappedBlock(DataBlock block)
        {
            D = block;
            Root = block.RootParent;
        }
    }
}