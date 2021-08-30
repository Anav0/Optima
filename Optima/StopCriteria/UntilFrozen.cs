using System;
using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;

namespace Optima.StopCriteria
{
    public class UntilFrozen<T> : IStopCriteria<T> where T : Solution
    {
        public const double MIN_TEMP = 1e-6;
        private readonly Cooler _cooler;

        public int Steps { get; private set; }

        public UntilFrozen(Cooler cooler)
        {
            _cooler = cooler ?? throw new ArgumentNullException(nameof(cooler));
        }

        public bool ShouldStop(T solution)
        {
            Steps++;
            return _cooler.Temperature < MIN_TEMP;
        }

        public void ResetToInitialState()
        {
            _cooler.ResetToInitialState();
        }

        public override string ToString()
        {
            return $"({nameof(UntilFrozen<T>)}): minimal temperature = {MIN_TEMP}";
        }
    }
}