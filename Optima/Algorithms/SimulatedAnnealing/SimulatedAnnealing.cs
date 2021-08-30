using System;
using Microsoft.Extensions.Logging;
using Optima.Algorithms.HillClimbing;
using Optima.Analysis;
using Optima.Base;
using Optima.Moves;
using Optima.StopCriteria;

namespace Optima.Algorithms.SimulatedAnnealing
{
    public class SimulatedAnnealing<T> : HillClimbingAlgorithm<T> where T : Solution
    {
        private readonly Random _rnd = new();
        private readonly SimulatedAnnealingSaver _saver;

        public Cooler Cooler { get; }

        public SimulatedAnnealing(Cooler cooler, Mover mover, IStopCriteria<T> stopCriteria,
            ILogger<SimulatedAnnealing<T>> logger,
            SimulatedAnnealingSaver saver = null) : base("SA", stopCriteria, mover, logger, saver)
        {
            Cooler = cooler;
            _saver = saver;
        }

        protected override void AtEndOfIteration(int iter)
        {
            _saver?.ChangeParams(iter, Cooler.Temperature);
            Cooler.Cool();
        }

        protected override bool AtEndOfIsMovedBetter(double diff)
        {
            var random = _rnd.NextDouble();
            var energy = Math.Exp(diff / Cooler.Temperature);

            return random < energy;
        }
    }
}