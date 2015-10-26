using System.Collections.Generic;

namespace ck2.Mapping.Save.Model
{
    public abstract class SaveObject
    {
        public void SerializeOrWrite()
        { }
        public IList<Property> Properties;
        public IList<Property> ByName;
    }
}