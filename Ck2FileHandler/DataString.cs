using System.Collections.Generic;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataString : IDataElement
    {
        string S;

        public DataString(string s, IDataElement parent)
        {
            S = s;
            Parent = parent;
        }

        public IList<IDataElement> Children => new List<IDataElement>(0);
        public IDataElement Parent { get; private set; }
        public int NestingLevel => Parent.NestingLevel + 1;
        public SaveFile SaveFile => Parent.SaveFile;
        public void ProcessLine(string line)
        {
            // Do Nothing
        }

        public static IDataElement From(string s, IDataElement parent)
        {
            return new DataString(s, parent);
        }


        public override string ToString()
        {
            return $"<<{S}>>";
        }
    }
}