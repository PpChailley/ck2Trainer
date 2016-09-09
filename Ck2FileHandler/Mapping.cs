using Ck2.Save.Model;

namespace Ck2.Save
{
    public class Mapping
    {
        private readonly DataBlock _root;

        public Mapping(DataBlock root)
        {
            _root = root;
        }

        public Character Player => new Character(_root.Block("character").Block(_root.Block("player").Value("id")));


    }
}