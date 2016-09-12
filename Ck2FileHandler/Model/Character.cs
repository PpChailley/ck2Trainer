using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class Character: MappedBlock
    {
        public Character(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public Dynasty Dynasty => new Dynasty(Root.Block("dynasties").Block(D.Value("dynasty")), M);
        public Property BirthName => D.Property("birth_name");
        public Property Government => D.Property("government");
        public int Id => int.Parse(D.Name);


        public bool Equals(Character c)
        {
            var idEquals = (c.Id == Id);
            return idEquals;
        }
    }
}