namespace Ck2.Save.Model
{
    public class Character: MappedBlock
    {
        public Character(DataBlock block) : base(block) { }

        public Dynasty Dynasty => new Dynasty(Root.Block("dynasties").Block(D.Value("dynasty")));



    }
}