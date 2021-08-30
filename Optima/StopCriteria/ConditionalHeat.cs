using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;

namespace Optima.StopCriteria
{
    public class ConditionalHeat<T> : IStopCriteria<T> where T : Solution
    {
        private readonly Cooler _cooler;
        private readonly float _heatBackByPercent;
        private readonly uint _maxSteps;
        private readonly OptimizationType _optimizationType;
        private double _bestSolValue;
        private readonly ushort _clock;
        private ushort _counter;
        public uint TotalSteps { get; private set; }

        public ConditionalHeat(Cooler cooler, OptimizationType optimizationType, uint maxSteps, float heatBackByPercent, ushort clock = 100)
        {
            _clock = clock;
            _counter = clock;
            _cooler = cooler;
            _maxSteps = maxSteps;
            _optimizationType = optimizationType;
            _heatBackByPercent = heatBackByPercent;

            _bestSolValue = _optimizationType == OptimizationType.Maximization ? double.MinValue : double.MaxValue;
            TotalSteps = 0;
        }

        public bool ShouldStop(T solution)
        {
            TotalSteps++;
            _counter--;

            if (TotalSteps > _maxSteps) return true;

            var isBetter = _optimizationType == OptimizationType.Maximization
                ? solution.CalculatedValue > _bestSolValue
                : solution.CalculatedValue < _bestSolValue;

            if (isBetter)
            {
                _bestSolValue = solution.CalculatedValue;
                _counter = _clock;
            }

            if (_counter == 0)
            {
                if (_cooler.Temperature < _cooler.InitialTemperature)
                    _cooler.Temperature *= _heatBackByPercent;
                _counter = _clock;
            }


            return false;
        }

        public void ResetToInitialState()
        {
            _bestSolValue = _optimizationType == OptimizationType.Maximization ? double.MinValue : double.MaxValue;
            TotalSteps = 0;
            _counter = _clock;
            _cooler.ResetToInitialState();
        }

        public override string ToString()
        {
            return $"({nameof(ConditionalHeat<T>)}): clock: {_clock}, max steps: {_maxSteps}";
        }
    }
}