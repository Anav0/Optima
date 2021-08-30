using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;

namespace Optima.StopCriteria
{
    public class Heater<T> : IStopCriteria<T> where T : Solution
    {
        private static readonly double _MIN_TEMP = 1e-6;
        private readonly Cooler _cooler;
        private readonly float _heatBackToPercent;
        private readonly uint _maxSteps;
        private readonly double _minTemp;
        private readonly OptimizationType _optimizationType;
        private double _bestSolValue;
        private double _prevMaxTemp;

        public uint TotalSteps { get; private set; }
        public uint StepsSinceReheat { get; private set; }

        public Heater(Cooler cooler, OptimizationType optimizationType, uint maxSteps, float heatBackToPercent, double minTemp = 1e-6)
        {
            _cooler = cooler;
            _prevMaxTemp = cooler.InitialTemperature;
            _maxSteps = maxSteps;
            _optimizationType = optimizationType;
            _heatBackToPercent = heatBackToPercent;
            _minTemp = minTemp;

            _bestSolValue = _optimizationType == OptimizationType.Maximization ? double.MinValue : double.MaxValue;
            TotalSteps = 0;
            StepsSinceReheat = 0;
        }

        public bool ShouldStop(T solution)
        {
            TotalSteps++;

            if (TotalSteps > _maxSteps) return true;

            var isBetter = _optimizationType == OptimizationType.Maximization
                ? solution.CalculatedValue > _bestSolValue
                : solution.CalculatedValue < _bestSolValue;

            if (isBetter)
                _bestSolValue = solution.CalculatedValue;

            if (_cooler.Temperature <= _MIN_TEMP || _cooler.Temperature <= _minTemp)
            {
                _cooler.Temperature = _prevMaxTemp * _heatBackToPercent;
                _prevMaxTemp = _cooler.Temperature;
            }

            return false;
        }

        public void ResetToInitialState()
        {
            _bestSolValue = _optimizationType == OptimizationType.Maximization ? double.MinValue : double.MaxValue;
            TotalSteps = 0;
            StepsSinceReheat = 0;
            _cooler.ResetToInitialState();
        }

        public override string ToString()
        {
            return $"({nameof(Heater<T>)}): heat by: {_heatBackToPercent}";
        }
    }
}