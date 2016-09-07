using System.Collections.Generic;

namespace Ck2.Save
{
    public interface ITextElement
    {
        IList<ITextElement> Children { get; }
        int NestingLevel { get; set; }
        void ProcessLine(string line);
    }
}