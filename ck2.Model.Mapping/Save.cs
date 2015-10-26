using System;

namespace ck2.Model.Mapping
{
    public class Save: SaveObject
    {
        public Player Player;
        
        
        public DynTitle DynTitle;
        public Flags Flags;
        public Dynasties Dynasties;
        public Characters Characters;

        public string PlayerRealm { get; set; }


    }
}
