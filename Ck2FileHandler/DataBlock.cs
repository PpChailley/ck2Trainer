using System.Collections.Generic;
using System.Text;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataBlock: IDataElement
    {
        public bool IsBlock => true;

        public IList<IDataElement> Children { get; }

        public IDataElement Parent { get; }
        public int NestingLevel { get; set; }
        public SaveFile SaveFile { get; set; }
        public string Name { get; set; }

        private bool _hasSeenOpeningBracket = false;

        public DataBlock(IDataElement parent, string beforeFirstLine = null)
        {
            Children = new List<IDataElement>(10000);
            Parent = parent;
            NestingLevel = parent?.NestingLevel +1 ?? 0;
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{GetType().Name} (Nest {NestingLevel}, Cnt {Children.Count}): ");

            if (Name != null)
            {
                sb.Append($"[{Name}]");
                return sb.ToString();
            }

            int childrenCount = 0;
            foreach (var child in Children)
            {
                sb.Append(child.ToString());

                if (childrenCount ++ > 4)
                    break;

                sb.Append(" -- ");
            }

            return sb.ToString();
        }

        public IEnumerable<IDataElement> GetDescendants(string name)
        {
            List<IDataElement> l = new List<IDataElement>(1000);

            foreach (var dataElement in Children)
            {
                if (dataElement.Name.Equals(name))
                {
                    l.Add(dataElement);
                }
                l.AddRange(dataElement.GetDescendants(name));
            }

            return l;
        }
    }
}