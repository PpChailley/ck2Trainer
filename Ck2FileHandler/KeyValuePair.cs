using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ck2.Mapping.Save.Extensions;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class KeyValuePair
    {

        public string Key { get; private set; }
        public IDataElement Value { get; private set; }


        public static KeyValuePair FromDataLine(DataLine dataLine)
        {
            if (dataLine == null || dataLine.AsText.Equals(string.Empty))
                return null;

            var results = dataLine.AsText.SplitAndKeep(new[] {'='}).ToArray();

            switch (results.Length)
            {
                // Line looks like "key=" or "key={"
                case 2:
                    return new KeyValuePair
                    {
                        Key = results[0],
                        Value = new DataBlock(dataLine.Parent) { Name = results[0] }
                    };

                // Line looks like "key=value"
                case 3:
                    return new KeyValuePair
                    {
                        Key = results[0],
                        Value = new DataString(dataLine.Parent, results[2])
                    };

                default:
                    return null;
            }
        }


        public override string ToString()
        {
            return $"KVP: {Key} => {Value}";
        }

        public string ToWritableString(int indentLevel)
        {
            var sb = new StringBuilder();

            sb.Append(new string('\t', indentLevel))
                .Append(Key)
                .Append("=")
                .Append(Value.ToUnindentedString());

            return sb.ToString();

        }


    }
}