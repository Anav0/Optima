using Optima.Base;
using Optima.Misc;

namespace Optima.StopCriteria
{
    public interface IStopCriteria<T> : IResetToInitial where T : Solution
    {
        bool ShouldStop(T solution);
    }
}