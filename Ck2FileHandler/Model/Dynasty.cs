namespace Ck2.Save.Model
{
    public class Dynasty: MappedBlock
    {
        public Dynasty(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public KeyValuePair Name => D.Property("name");
    }
}