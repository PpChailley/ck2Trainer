using System;
using System.Collections.Generic;
using System.IO;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class TextBlock: ITextElement
    {
        public IList<ITextElement> Children { get; set; }
        public int NestingLevel { get; set; }
        public SaveFile SaveFile { get; set; }
        public ITextElement Parent;
        private  bool _reachedEndOfBlock = false;

        public TextBlock(ITextElement parent, SaveFile saveFile, string beforeFirstLine = null)
        {
            Children = new List<ITextElement>(1000);
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
                footer = line.Substring(line.IndexOf("}", StringComparison.Ordinal) + 1);
                line = line.Substring(0, line.IndexOf("}", StringComparison.Ordinal));
            }

            AddText(line);
            AddChild(inside);
            if (footer != null)
            {
                Parent?.ProcessLine(footer);
                _reachedEndOfBlock = true;
            }
            
        }

        private void AddChild(string line)
        {
            if (line == null) return;
            // line = line.Trim();
            // if (line.Equals(string.Empty)) return;

            Children.Add(new TextBlock(this, SaveFile, line));
        }

        private void AddText(string line)
        {
            if (line == null) return;
            line = line.Trim();
            if (line.Equals(string.Empty)) return;

            Children.Add(new TextLine(line));
        }

        public override string ToString()
        {
            return $"{GetType().Name} (Nest {NestingLevel}) - {Children.Count} elts";
        }

    }
}