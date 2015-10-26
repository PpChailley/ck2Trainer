namespace ck2.Mapping.Save.Model
{
    public class Save: SaveObject
    {
        public Player Player;
        
        
        public DynTitle DynTitle;
        public Flags Flags;
        public Dynasties Dynasties;
        public Character Characters;

        public string PlayerRealm { get; set; }


    }
}
