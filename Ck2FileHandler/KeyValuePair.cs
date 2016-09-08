using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class KeyValuePair
    {

        public string Key { get; private set; }
        public IDataElement Value { get; private set; }


        public static KeyValuePair FromDataLine(DataLine dataLine)
        {
            if (dataLine == null || dataLine.Text.Equals(string.Empty))
                return null;

            var matches = Regex.Matches(dataLine.Text, @"(.+)=(.*)");

            if (matches.Count != 1)
                return null;

            var kv = new KeyValuePair {Key = matches[0].Groups[1].Value};
            var valueString = matches[0].Groups[2].Value;

            if (valueString.Equals(string.Empty))
            {
                kv.Value = new DataBlock(dataLine.Parent) { Name = kv.Key };
            }
            else
            {
                kv.Value = new DataString(dataLine.Parent, valueString);
            }

            return kv;

        }


        public override string ToString()
        {
            return $"KVP: {Key} => {Value}";
        }





    }
}