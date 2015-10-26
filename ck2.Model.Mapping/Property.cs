using System;

namespace ck2.Model.Mapping
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