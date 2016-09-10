using System.Collections.Generic;
using System.Linq;

namespace Ck2.Save.Model
{
    public class Mapping
    {
        private readonly DataBlock _root;

        public Mapping(DataBlock root)
        {
            _root = root;
        }

        public int PlayerId => int.Parse(_root.Block("player").Value("id"));
        public Character Player => new Character(_root.Block("character").Block(PlayerId), this);

        public KeyValuePair Date => _root.Property("date");

        public IEnumerable<Character> Characters => _root.Block("character").Blocks().Select(b => new Character(b, this));

        public IEnumerable<Army> AllArmies => _root.GetDescendants("army").Select( data => new Army(data.AsBlock, this));

    }
}