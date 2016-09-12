using System.Collections.Generic;
using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    public class Mapping
    {
        private readonly DataBlock _root;

        public Mapping(DataBlock root)
        {
            _root = root;
        }

        private int? _playerId;
        public int PlayerId => _playerId?? (int) (_playerId = int.Parse(_root.Block("player").Value("id")));

        private Character _player;
        public Character Player => _player ?? (_player = new Character(_root.Block("character").Block(PlayerId), this));


        public Property Date => _root.Property("date");

        private IEnumerable<Character> _characters;
        public IEnumerable<Character> Characters => _characters?? (_characters = _root.Block("character").Blocks().Select(b => new Character(b, this)));

        public IEnumerable<Army> AllArmies => _root.GetDescendants("army").Select( data => new Army(data.AsBlock, this));

    }
}