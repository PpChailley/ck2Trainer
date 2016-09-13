using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ck2.Save.File
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataBlock: AbstractDataElement, IDataElement
    {
        public bool IsBlock => true;
        public DataBlock AsBlock => this;

        public IList<IDataElement> Children { get; }

        public override IDataElement Parent { get; }
        public int NestingLevel { get; set; }
        public SaveFile SaveFile { get; set; }
        public string Name { get; set; }


        private bool _hasSeenOpeningBracket = false;

        public DataBlock(IDataElement parent)
        {
            Children = new List<IDataElement>();
            Parent = parent;
            NestingLevel = parent?.NestingLevel +1 ?? 0;
        }



        private readonly Dictionary<string, IEnumerable<Property>> _propertiesCache = new Dictionary<string, IEnumerable<Property>>();

        public IEnumerable<Property> Properties(string name = null)
        {
            IEnumerable<Property> cached;
            if (_propertiesCache.TryGetValue(name??"null", out cached))
                return cached;

            var found = Children.OfType<DataLine>()
                .Where(c => name == null || c.Name.Equals(name))
                .Select(line => line.AsKeyVal)
                .ToArray();

            _propertiesCache.Add(name??"null", found);
            return found;

        }

        public IEnumerable<DataBlock> Blocks(string name = null)
        {
            return Properties(name).Select(p => p.Value.AsBlock);
        }

        public IEnumerable<string> Values(string name = null)
        {
            return Properties(name).Select(p => p.Value.ToUnindentedString());
        }

        public Property Property(string name)
        {
            return Properties(name).Single();
        }

        public DataBlock Block(int id) { return Block(id.ToString()); }
        public DataBlock Block(string name)
        {
            return Blocks(name).Single();
        }
        


        public string Value(string name)
        {
            return Values(name).Single();
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
                    newBornChild.Name = $"Unnamed Child of ({Name})";
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
            return GetDescendants(name == null ? null : new []{name} );
        }


        public IEnumerable<IDataElement> GetDescendants(string[] name)
        {
            List<IDataElement> l = new List<IDataElement>(1000);

            foreach (IDataElement dataElement in Children)
            {
                if (name == null || name.Contains(dataElement.Name))
                {
                    l.Add(dataElement);
                }


                if (dataElement.AsBlock != null)
                    l.AddRange(dataElement.AsBlock.GetDescendants(name));
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