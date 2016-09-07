using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class KeyValuePair : IDataElement
    {
        private readonly DataLine _line;
        public SaveFile SaveFile => _line.SaveFile;


        public string Key { get; private set; }
        public IDataElement Value { get; private set; }

        private KeyValuePair(string key, string value, DataLine line)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));

            Key = key;
            _line = line;
            Value = DataString.From(value, Parent);

            if (value.Trim().Equals(string.Empty))
            {
                ExpectValueFromNextLine();
            }
        }

        private void ExpectValueFromNextLine()
        {
            var valueBlock = new DataBlock(this, _line.Parent.SaveFile);
            valueBlock.Name = Key;
            Value = valueBlock;
            Children.Add(Value);
        }

        public IList<IDataElement> Children => new List<IDataElement>(0);
        public IDataElement Parent => _line.Parent;
        public int NestingLevel => _line.NestingLevel;

 

        public void ProcessLine(string line)
        {
            Parent.ProcessLine(line);
        }


        public static IDataElement FromDataLine(DataLine line)
        {
            if (line == null)
            {
                return null;
            }

            var matches = Regex.Matches(line.Text, @"(.+)=(.*)");

            if (matches.Count == 1)
            {
                var key = matches[0].Groups[1].Value;
                var valueString = matches[0].Groups[2].Value;
                return new KeyValuePair(key, valueString, line);
            }

            return null;
        }


        public override string ToString()
        {
            return $"KVP: {Key} => {Value}";
        }
    }
}