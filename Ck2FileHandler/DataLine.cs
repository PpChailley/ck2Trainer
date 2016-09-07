using System;
using System.Collections.Generic;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataLine : IDataElement
    {
        public string Text;
        public IList<IDataElement> Children => new IDataElement[0];

        public IDataElement Parent { get; }
        public SaveFile SaveFile => Parent.SaveFile;
        public int NestingLevel { get; }

        public void ProcessLine(string line)
        {
            throw new InvalidOperationException();
        }


        public DataLine(string text, IDataElement parent, int nestingLevel)
        {
            Parent = parent;
            NestingLevel = nestingLevel;
            Text = text.Trim();
        }

        public override string ToString()
        {
            return GetType().Name + " : " + Text;
        }

        public IDataElement ToBestRepresentation()
        {
            var keyval = KeyValuePair.FromDataLine(this);
            return keyval ?? this;
        }
    }
}