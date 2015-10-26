using System;

namespace ck2.Mapping.Save.Model
{
    public class Property
    {
        public enum Value
        {
            Yes,
            No,
            Unset
        }

        public String Name;
        public Property.Value Val;

    }
}