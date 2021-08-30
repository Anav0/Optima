using System;
using Optima.Base;

namespace Optima.Criteria.Objective
{
    public class WeightedObjectiveAggregator<T> : ObjectiveAggregator<T> where T : Solution
    {
        private readonly double[] _weights;

        public WeightedObjectiveAggregator(Objective<T>[] objectives) : base(objectives)
        {
            _weights = new double[objectives.Length];
            for (var i = 0; i < objectives.Length; i++) _weights[i] = 1.0;
        }

        public WeightedObjectiveAggregator(Objective<T>[] objectives, double[] weights) : base(objectives)
        {
            if (weights.Length != objectives.Length)
                throw new Exception("Weights are not the same length as number of objectives");

            _weights = weights;
        }

        protected override double CalculateValue(T solution)
        {
            double sum = 0;
            for (var i = 0; i < _weights.Length; i++)
                sum += _weights[i] * Objectives[i].CalculateValue(solution);

            return sum;
        }
    }
}