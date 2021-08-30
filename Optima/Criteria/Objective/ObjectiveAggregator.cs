using System;
using Optima.Base;

namespace Optima.Criteria.Objective
{
    public abstract class ObjectiveAggregator<T> where T : Solution
    {
        protected readonly Objective<T>[] Objectives;

        // ReSharper disable once UnusedMember.Global
        public ObjectiveAggregator()
        {
            //Used by Mock<> 
        }

        protected ObjectiveAggregator(Objective<T>[] objectives)
        {
            if (objectives == null || objectives.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(objectives));

            Objectives = objectives;
        }

        public double Value(T solution)
        {
            if (solution.ValueWasCalculated) return solution.CalculatedValue;

            solution.CalculatedValue = CalculateValue(solution);
            solution.ValueWasCalculated = true;

            return solution.CalculatedValue;
        }

        protected abstract double CalculateValue(T solution);
    }
}