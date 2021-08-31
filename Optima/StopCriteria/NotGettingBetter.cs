using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;

namespace Optima.StopCriteria
{
    public class NotGettingBetter<T> : IStopCriteria<T> where T : Solution
    {
        private readonly Cooler _cooler;
        private readonly uint _maxSteps;
        private readonly uint _notGettingBetterDuration;
        private readonly OptimizationType _optimizationType;
        private uint _bestSolUpdatedAtStep;
        private double _bestSolValue;

        public uint Steps { get; private set; }

        public NotGettingBetter(Cooler cooler, OptimizationType optimizationType, uint maxSteps,
            uint notGettingBetterDuration = 1000)
        {
            _bestSolValue = optimizationType == OptimizationType.Maximization ? double.MinValue : double.MaxValue;
            _cooler = cooler;
            _notGettingBetterDuration = notGettingBetterDuration;
            _optimizationType = optimizationType;
            _maxSteps = maxSteps;
        }

        public bool ShouldStop(T solution)
        {
            Steps++;

            if (Steps > _maxSteps) return true;

            var isBetter = _optimizationType == OptimizationType.Maximization
                ? solution.CalculatedValue > _bestSolValue
                : solution.CalculatedValue < _bestSolValue;

            if (isBetter)
            {
                _bestSolValue = solution.CalculatedValue;
                _bestSolUpdatedAtStep = Steps;
            }

            if (Steps - _bestSolUpdatedAtStep > _notGettingBetterDuration) return true;

            return false;
        }

        public void ResetToInitialState()
        {
            _bestSolValue = _optimizationType == OptimizationType.Maximization ? double.MinValue : double.MaxValue;
            _bestSolUpdatedAtStep = 0;
            Steps = 0;
            _cooler.ResetToInitialState();
        }

        public override string ToString()
        {
            return $"{nameof(NotGettingBetter<T>)}\n\tMax iterations with not improvement = {_notGettingBetterDuration.ToString("N")}\n\tMax steps = {_maxSteps.ToString("N")}";
        }
    }
}