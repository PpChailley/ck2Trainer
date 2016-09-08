using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ck2.Save
{
    public struct CallerContext
    {
        public CancellationToken CancelToken;
        //public IProgress<int> Progress;
        public Action<int> ProgressReport;
        //public ParallelLoopState LoopState;

        public static CallerContext Empty => new CallerContext()
        {
            CancelToken = new CancellationToken(),
            // LoopState = null,
            // Progress = new Progress<int>()
            ProgressReport = (i => { })
        };
    }
}