using System;
using System.Collections.Generic;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataString : IDataElement
    {
        private string _s;

        public string Name => string.Empty;
        public bool IsBlock => false;


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

        public IEnumerable<IDataElement> GetDescendants(string name)
        {
            return new IDataElement[] {};
        }


        public override string ToString()
        {
            return $"<<{_s}>>";
        }
    }
}