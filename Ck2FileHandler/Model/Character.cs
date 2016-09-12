using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class Character: MappedBlock
    {
        public Character(DataBlock block, Mapping mapping) : base(block, mapping) { }

        public Dynasty Dynasty => new Dynasty(Root.Block("dynasties").Block(D.Value("dynasty")), M);
        public Property BirthName => D.Property("birth_name");
        public Property Government => D.Property("government");

        private int? _id;
        public int Id => _id?? (int) (_id = int.Parse(D.Name) as int?);


        public bool Equals(Character c)
        {
            var idEquals = (c.Id == Id);
            return idEquals;
        }
    }
}