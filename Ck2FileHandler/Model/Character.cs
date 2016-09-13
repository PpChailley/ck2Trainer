using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class Character: MappedBlock
    {
        public Character(DataBlock block, Mapping mapping) : base(block, mapping) { }

        private bool _dynastyIsFound = false;
        private Dynasty _dynasty;
        public Dynasty Dynasty
        {
            get
            {
                if (_dynastyIsFound)
                    return _dynasty;

                var dynastyId = D.Values("dynasty").SingleOrDefault();

                _dynasty = dynastyId == null ? null : M.Dynasties.Single(dyn => dyn.Id == int.Parse(dynastyId));

                _dynastyIsFound = true;
                return _dynasty;
            }
        }
            
            
            

        public Property BirthName => D.Property("birth_name");
        public Property Government => D.Property("government");

        private int? _id;
        public int Id => _id?? (int) (_id = int.Parse(D.Name) as int?);

        private Demesne _demesne;
        private bool _demesmeIsCached = false;
        public Demesne Demesne
        {
            get
            {
                if (_demesmeIsCached)
                    return _demesne;
                
                var demesmeBlock = D.Blocks("demesne").SingleOrDefault();

                if (demesmeBlock == null)
                {
                    _demesmeIsCached = true;
                    _demesne = null;
                    return null;
                }

                _demesne = new Demesne(demesmeBlock, M);
                _demesmeIsCached = true;
                return _demesne;
            }
        }
            


        public bool Equals(Character c)
        {
            var idEquals = (c.Id == Id);
            return idEquals;
        }

        public override string ToString()
        {
            return $"[Char id={Id} '{BirthName} {Dynasty?.Name}']";
        }
    }
}