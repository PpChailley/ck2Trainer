namespace Ck2.Save.Model
{
    public class Dynasty: MappedBlock
    {
        public Dynasty(DataBlock block) : base(block) { }
        public KeyValuePair Name => D.Property("name");
    }
}