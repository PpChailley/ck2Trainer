using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class DataBlock: IDataElement
    {
        public IList<IDataElement> Children { get; set; }
        public IDataElement Parent { get; }
        public int NestingLevel { get; set; }
        public SaveFile SaveFile { get; set; }
        private  bool _reachedEndOfBlock = false;
        public string Name { get; set; }

        public DataBlock(IDataElement parent, SaveFile saveFile, string beforeFirstLine = null)
        {
            Children = new List<IDataElement>(1000);
            Parent = parent;
            NestingLevel = parent?.NestingLevel +1 ?? 0;
            SaveFile = saveFile;
            
            BuildFrom(saveFile, beforeFirstLine, null);
        }



        private void BuildFrom(SaveFile file, string before, string after)
        {
            if (before != null)
            {
                ProcessLine(before);
            }

            while (file.EndOfStream == false && _reachedEndOfBlock == false) 
            {
                var line = file.ReadLine();
                ProcessLine(line);
            } 
        }


        public void ProcessLine(string line)
        {
            var originalLine = line;

            if (line == null) return;

            string inside = null;
            string footer = null;

            if (line.Contains("{"))
            {
                inside = line.Substring(line.IndexOf("{", StringComparison.Ordinal) + 1);
                line = line.Substring(0, line.IndexOf("{", StringComparison.Ordinal));

                if (inside.Contains("{") )
                { throw new InvalidOperationException("One single line cannot contain two brackets."); }
            }

            if (line.Contains("}"))
            {
                footer = line.Substring(line.IndexOf("}", StringComparison.Ordinal));
                line = line.Substring(0, line.IndexOf("}", StringComparison.Ordinal));
            }

            AddText(line);
            AddChild(inside);
            if (footer != null)
            {
                if (footer.StartsWith("}"))
                {
                    footer = footer.Substring(1);
                    _reachedEndOfBlock = true;
                }

                Parent?.ProcessLine(footer);
            }
            
        }

        private void AddChild(string line)
        {
            if (line == null) return;
            // line = line.Trim();
            // if (line.Equals(string.Empty)) return;

            Children.Add(new DataBlock(this, SaveFile, line));
        }

        private void AddText(string line)
        {
            if (line == null) return;
            line = line.Trim();
            if (line.Equals(string.Empty)) return;

            Children.Add(new DataLine(line, this, NestingLevel + 1).ToBestRepresentation());
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

    }
}