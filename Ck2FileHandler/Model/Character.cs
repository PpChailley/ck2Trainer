namespace Ck2.Save.Model
{
    public class Character: MappedBlock
    {
        public Character(DataBlock block) : base(block) { }

        public Dynasty Dynasty => new Dynasty(Root.Block("dynasties").Block(D.Value("dynasty")));
        public KeyValuePair BirthName => D.Property("birth_name");
        public KeyValuePair Government => D.Property("government");
    }
}