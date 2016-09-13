using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ck2.Save.File;
using NUnit.Framework;

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

        private List<Army> _allArmies;
        public IEnumerable<Army> AllArmies
        {
            get
            {
                if (_allArmies != null)
                    return _allArmies;

                _allArmies = new List<Army>(100);

                foreach (var c in Characters.ToArray())
                {
                    if (c.Demesne != null)
                    {
                        _allArmies.AddRange(c.Demesne.Armies);
                        foreach (var navy in c.Demesne.Navies)
                        {
                            _allArmies.AddRange(navy.Armies);
                        }
                    }

                }


                // TODO: remove reference armies
                /*
                foreach (var title in Titles)
                {
                    _allArmies.AddRange(title.Armies);
                }*/



                return _allArmies;

//                    allArmyBlocks = _root.GetDescendants("army").Select(data => new Army(data.AsBlock, this));
            }
        }

        private IEnumerable<Title> _titles;
        public IEnumerable<Title> Titles => _titles ?? (_titles = _root.Block("title").Blocks().Select(b => new Title(b, this)));


        private IEnumerable<Dynasty> _dynasties;
        public IEnumerable<Dynasty> Dynasties => _dynasties ?? (_dynasties = _root.Block("dynasties").Blocks().Select(block => new Dynasty(block, this)));

    }
}