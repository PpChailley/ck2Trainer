using System.Collections.Generic;

namespace Ck2.Save.File
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataString : AbstractDataElement, IDataElement
    {
        private string _s;

        public string Name => string.Empty;
        public bool IsBlock => false;
        public DataBlock AsBlock => null;


        public DataString(IDataElement parent)
        {
            Parent = parent;
        }

        public DataString(IDataElement parent, string valueString) : this(parent)
        {
            if (valueString.StartsWith("\"") && valueString.EndsWith("\""))
            {
                _s = valueString.Substring(1, valueString.Length - 2);
            }
            else
            {
                _s = valueString;
            }
        }

        public IList<IDataElement> Children => new List<IDataElement>(0);
        public override IDataElement Parent { get; }
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

        public string ToIndentedString()
        {
            return new string('\t', NestingLevel) + _s;
        }

        public string ToUnindentedString()
        {
            return _s;
        }


        public override string ToString()
        {
            return $"<<{_s}>>";
        }
    }

    public abstract class AbstractDataElement
    {
        public abstract IDataElement Parent { get; }



        public DataBlock RootParent 
        {
            get
            {
                var p = this;
                while (p.Parent != null)
                {
                    p = (DataBlock)p.Parent;
                }
                return (DataBlock) p;
            }
        }

    }
}