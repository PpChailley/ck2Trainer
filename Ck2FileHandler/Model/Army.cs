using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Ck2.Save.File;

namespace Ck2.Save.Model
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class Army : MappedBlock
    {

        public Army(DataBlock block, Mapping mapping) : base(block, mapping) { }


        public bool IsReferenceArmy => D.Blocks("name").Any();

        private Army _definition;
        /// <summary>
        /// Dereferences this Army if needed
        /// </summary>
        public Army Def
        {
            get
            {
                if (_definition != null)
                    return _definition;

                _definition = LocateMainBlock();

                return _definition;
            }
        }

        private Army LocateMainBlock()
        {
            if (IsReferenceArmy == false)
                return this;

            var armiesFullBlocks = M.AllArmies.Where(a => a.D.Children.Count > 3).ToArray();
            var found = armiesFullBlocks.Where(a => ParseId(a.D.AsBlock).Equals(Id)).ToArray();

            return found.Single();
        }

        private static int ParseId(DataBlock armyBlock)
        {
            var prop = armyBlock.Property("id");
            int armyId;

            if (prop.Value is DataBlock)
                armyId = int.Parse(prop.Value.AsBlock.Value("id"));
            else
                armyId = int.Parse(prop.Value.ToUnindentedString());

            return armyId;
        }

        private bool _idIsSet;
        private int? _id;
        public int? Id
        {
            get
            {
                if (_idIsSet)
                    return _id;

                var foundId = D.Property("id");
                if (foundId == null)
                    _id = null;
                else
                    _id = ParseId(D);

                _idIsSet = true;
                return _id;
            }
        }

        private int? _morale;
        public int Morale => _morale ?? (int)(_morale = int.Parse(D.Value("morale")));

        private int _ownerId;
        private bool _ownerIdIsSet = false;
        public int OwnerId => _ownerIdIsSet ? _ownerId : (_ownerId = int.Parse(D.Value("owner")));

        private Character _owner;
        public Character Owner => _owner ?? (_owner = M.Characters.Single(c => c.Id == OwnerId));

        private IEnumerable<Army> _subUnits;
        public IEnumerable<Army> SubUnits => _subUnits?? (_subUnits = D.Blocks("sub_unit").Select(block => new Army(block, M)));


        public override string ToString()
        {
            string refMarker = IsReferenceArmy? " REF" : " FULL";

            return $"[Army id={Id}{refMarker}]";
        }
    }
}