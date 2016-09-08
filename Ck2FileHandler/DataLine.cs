using System;
using System.Collections.Generic;
using System.Text;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataLine : IDataElement
    {
        public string Name => AsKeyVal != null ? AsKeyVal.Key : string.Empty;

        public IEnumerable<IDataElement> GetDescendants(string name)
        {
            return new IDataElement[] { };
        }



        public bool IsBlock => AsKeyVal?.Value.IsBlock ?? false;

        public string AsText;
        public KeyValuePair AsKeyVal;
        public bool HasTriedKeyVal = false;

        public IList<IDataElement> Children => AsKeyVal == null ? new IDataElement[0] : AsKeyVal.Value.Children;

        public IDataElement Parent { get; }
        public int NestingLevel { get; }


        public DataLine(IDataElement parent, int nestingLevel)
        {
            Parent = parent;
            NestingLevel = nestingLevel;
        }


        public IDataElement ProcessLine(string line)
        {
            AsText = line.Trim();
            HasTriedKeyVal = false;
            ToBestRepresentation();

            if (AsKeyVal != null  && AsKeyVal.Value.IsBlock)
                return AsKeyVal.Value;
            else
                return Parent;
        }

        private void ToBestRepresentation()
        {
            if (HasTriedKeyVal == true)
                return;

            var keyval = KeyValuePair.FromDataLine(this);
            AsKeyVal = keyval;
            HasTriedKeyVal = true;
        }


        public override string ToString()
        {
            if (AsKeyVal == null)
                return $"DL(t) <{AsText}>";
            else
                return $"DL(kv) <{AsKeyVal}>";
        }

        public string ToIndentedString()
        {
            if (AsKeyVal == null)
                return new string('\t', NestingLevel) + AsText;
            else
                return new string('\t', NestingLevel) + AsKeyVal.ToWritableString(0);
        }

        public string ToUnindentedString()
        {
            if (AsKeyVal == null)
                return AsText;
            else
                return AsKeyVal.ToWritableString(0);
        }
    }
}