using System.Collections.Generic;

namespace Ck2.Save.File

{
    public interface IDataElement: IDataContainer
    {
        IList<IDataElement> Children { get; }
        IDataElement Parent { get; }
        int NestingLevel { get; }
        string Name { get; }

        bool IsBlock { get; }
        DataBlock AsBlock { get;  }



        /// <summary>
        /// Process the given data line and return a pointer to the new target (new child, itself or parent if just closed)
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        IDataElement ProcessLine(string line);

        IEnumerable<IDataElement> GetDescendants(string name);

        string ToIndentedString();
        string ToUnindentedString();

        DataBlock RootParent { get; }
    }
}