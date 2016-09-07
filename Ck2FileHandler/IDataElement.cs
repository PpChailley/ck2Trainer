using System;
using System.Collections.Generic;

namespace Ck2.Save
{
    public interface IDataElement
    {
        IList<IDataElement> Children { get; }
        IDataElement Parent { get; }
        int NestingLevel { get; }
        void ProcessLine(string line);
        SaveFile SaveFile { get; }
    }
}