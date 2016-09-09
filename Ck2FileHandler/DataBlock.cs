using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataBlock: AbstractDataElement, IDataElement
    {
        public bool IsBlock => true;

        public IList<IDataElement> Children { get; }

        public override IDataElement Parent { get; }
        public int NestingLevel { get; set; }
        public SaveFile SaveFile { get; set; }
        public string Name { get; set; }


        private bool _hasSeenOpeningBracket = false;

        public DataBlock(IDataElement parent, string beforeFirstLine = null)
        {
            Children = new List<IDataElement>();
            Parent = parent;
            NestingLevel = parent?.NestingLevel +1 ?? 0;
        }


        public DataBlock Block(string name)
        {
            var block = ((DataBlock) GetChildNamed(name));
            return block;
        }

        public string Value(string name)
        {
            var s = ((DataString) GetChildNamed(name));
            return s.ToUnindentedString();
        }

        private IDataElement GetChildNamed(string name)
        {
            return Children.OfType<DataLine>()
                .Single(c => c.Name.Equals(name))
                .AsKeyVal.Value;
        }




        public IDataElement ProcessLine(string line)
        {
            if (line == null)
                return this;

            if (line.Equals("{"))
            {
                if (_hasSeenOpeningBracket == false)
                {
                    _hasSeenOpeningBracket = true;
                    return this;
                }
                else
                {
                    var newBornChild = new DataBlock(this);
                    Children.Add(newBornChild);
                    newBornChild.Name = $"Unnamed Child of ({this.Name})";
                    return newBornChild;
                }
                
            }
            else if (line.Equals("}"))
            {
                return Parent;
            }
            else
            {
                return AddText(line);
            }
        }

        private void AddChild(string line)
        {
            if (line == null) return;
            Children.Add(new DataBlock(this, line));
        }

        private IDataElement AddText(string line)
        {
            if (line == null)
                return this;
            line = line.Trim();
            if (line.Equals(string.Empty))
                return this;

            var dataLine = new DataLine(this, NestingLevel + 1);
            Children.Add(dataLine);

            return dataLine.ProcessLine(line);
        }


        public IEnumerable<IDataElement> GetDescendants(string name)
        {
            List<IDataElement> l = new List<IDataElement>(1000);

            foreach (var dataElement in Children)
            {
                if (name == null || dataElement.Name.Equals(name))
                {
                    l.Add(dataElement);
                }
                l.AddRange(dataElement.GetDescendants(name));
            }

            return l;
        }


        public override string ToString()
        {
            return $"B[{NestingLevel},{Children.Count},'{Name}']";
        }



        public string ToIndentedString()
        {
            return ToUnindentedString();
        }

        public string ToUnindentedString()
        {
            var sb = new StringBuilder();

            sb.AppendLine();

            sb.Append(new string('\t', NestingLevel))
                .AppendLine("{");

            foreach (var child in Children)
            {
                sb.AppendLine(child.ToIndentedString());
            }

            sb.Append(new string('\t', NestingLevel))
                .Append("}");

            return sb.ToString();

        }

    }
}