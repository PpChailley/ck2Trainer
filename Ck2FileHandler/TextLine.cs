using System;
using System.Collections.Generic;

namespace Ck2.Save
{
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    internal class TextLine : ITextElement
    {
        public string Text;
        public IList<ITextElement> Children => new ITextElement[0];
        public int NestingLevel { get; set; }

        public void ProcessLine(string line)
        {
            throw new InvalidOperationException();
        }

        public TextLine(string text)
        {
            Text = text.Trim();
        }

        public override string ToString()
        {
            return GetType().Name + " : " + Text;
        }
    }
}