using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ck2.Save
{
    public struct CallerContext
    {
        public CancellationToken Cancel;
        //public IProgress<int> Progress;
        public Action<int> ProgressReport;
        //public ParallelLoopState LoopState;

        public static CallerContext Empty => new CallerContext()
        {
            Cancel = new CancellationToken(),
            // LoopState = null,
            // Progress = new Progress<int>()
            ProgressReport = (i => { })
        };
    }
}