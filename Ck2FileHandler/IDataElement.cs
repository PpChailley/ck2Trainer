using System;
using System.Collections.Generic;

namespace Ck2.Save
{
    public interface IDataElement
    {
        IList<IDataElement> Children { get; }
        IDataElement Parent { get; }
        int NestingLevel { get; }

        bool IsBlock { get; }


        /// <summary>
        /// Process the given data line and return a pointer to the new target (new child, itself or parent if just closed)
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        IDataElement ProcessLine(string line);

    }
}