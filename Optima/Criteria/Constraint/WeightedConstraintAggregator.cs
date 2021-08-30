using System;
using Optima.Base;

namespace Optima.Criteria.Constraint
{
    public class WeightedConstraintAggregator<T> : ConstraintAggregator<T> where T : Solution
    {
        private readonly double[] _weights;

        public WeightedConstraintAggregator(Constraint<T>[] constraints) : base(constraints)
        {
            _weights = new double[constraints.Length];
            for (var i = 0; i < constraints.Length; i++) _weights[i] = 1.0;
        }

        public WeightedConstraintAggregator(Constraint<T>[] constraints, double[] weights) : base(constraints)
        {
            if (weights.Length != constraints.Length)
                throw new Exception("Weights are not the same length as number of constraints");

            _weights = weights;
        }

        protected override double CalculatePenalty(T solution)
        {
            double sum = 0;
            for (var i = 0; i < _weights.Length; i++)
                sum += _weights[i] * Constraints[i].Penalty(solution);

            return sum;
        }

        protected override double InitialCalculation(T solution)
        {
            double sum = 0;
            for (var i = 0; i < _weights.Length; i++)
                sum += _weights[i] * Constraints[i].Initial(solution);

            return sum;
        }
    }
}