using Optima.Base;

namespace Optima.StopCriteria
{
    public class MaxSteps<T> : IStopCriteria<T> where T : Solution
    {
        private readonly uint _maxSteps;

        public virtual uint Steps { get; protected set; }

        public MaxSteps()
        {
        }

        public MaxSteps(uint maxSteps)
        {
            _maxSteps = maxSteps;
        }

        public bool ShouldStop(T solution)
        {
            Steps++;
            return Steps > _maxSteps;
        }

        public void ResetToInitialState()
        {
            Steps = 0;
        }

        public override string ToString()
        {
            return $"({nameof(MaxSteps<T>)}): maxSteps = {_maxSteps}";
        }
    }
}