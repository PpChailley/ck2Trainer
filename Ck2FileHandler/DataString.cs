using System.Collections.Generic;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataString : IDataElement
    {
        public bool IsBlock => false;
        private string _s;

        public DataString(IDataElement parent)
        {
            Parent = parent;
        }

        public DataString(IDataElement parent, string valueString) : this(parent)
        {
            _s = valueString;
        }

        public IList<IDataElement> Children => new List<IDataElement>(0);
        public IDataElement Parent { get; private set; }
        public int NestingLevel => Parent.NestingLevel + 1;

        public IDataElement ProcessLine(string line)
        {
            _s = line;
            return Parent;
        }

        

        public override string ToString()
        {
            return $"<<{_s}>>";
        }
    }
}