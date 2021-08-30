using Microsoft.Extensions.Logging;
using Optima.Base;
using Optima.Criteria;
using Optima.Misc;

namespace Optima.Algorithms
{
    public abstract class OptimizationAlgorithm<T> : IResetToInitial where T : Solution
    {
        protected readonly ILogger Logger;
        public string Name { get; }

        protected OptimizationAlgorithm(string name, ILogger<OptimizationAlgorithm<T>> logger)
        {
            Logger = logger;
            Name = name;
        }

        public abstract void ResetToInitialState();

        public abstract T Solve(T solution, Criterion<T> criterion);
    }
}