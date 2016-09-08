using System;
using System.Collections.Generic;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataLine : IDataElement
    {
        public bool IsBlock => false;

        public string Text;
        public IList<IDataElement> Children => new IDataElement[0];

        public IDataElement Parent { get; }
        public int NestingLevel { get; }

        public KeyValuePair AsKeyVal;
        public bool HasTriedKeyVal = false;

        public DataLine(IDataElement parent, int nestingLevel)
        {
            Parent = parent;
            NestingLevel = nestingLevel;
        }


        public IDataElement ProcessLine(string line)
        {
            Text = line.Trim();
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
                return GetType().Name + " : " + Text;
            else
                return AsKeyVal.ToString();
        }

    }
}