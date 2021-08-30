using System;
using System.ComponentModel;
using Optima.Base;
using Optima.Criteria.Constraint;
using Optima.Criteria.Objective;

namespace Optima.Criteria
{
    public sealed class Criterion<T> where T : Solution
    {
        private readonly ConstraintAggregator<T> _constraintAggr;
        private readonly ObjectiveAggregator<T> _objectiveAggr;
        public readonly OptimizationType OptimizationType;

        public Criterion(OptimizationType optimizationType, ObjectiveAggregator<T> objectiveAggr, ConstraintAggregator<T> constraintAggr)
        {
            if (!Enum.IsDefined(typeof(OptimizationType), optimizationType))
                throw new InvalidEnumArgumentException(nameof(optimizationType), (int) optimizationType, typeof(OptimizationType));

            _objectiveAggr = objectiveAggr ?? throw new ArgumentNullException(nameof(objectiveAggr));
            _constraintAggr = constraintAggr ?? throw new ArgumentNullException(nameof(constraintAggr));
            OptimizationType = optimizationType;
        }

        public void InitialEvaluation(T solution)
        {
            _constraintAggr.Initial(solution);

            if (solution.IsFeasible)
                _objectiveAggr.Value(solution);
        }

        public double CalculateSolutionValue(T solution)
        {
            return _objectiveAggr.Value(solution);
        }

        public double CalculateSolutionPenalty(T solution)
        {
            return _constraintAggr.Penalty(solution);
        }

        public bool IsFirstOneBetter(T firstSolution, T secondSolution)
        {
            if (secondSolution == null)
                return true;

            if (firstSolution.IsFeasible && !secondSolution.IsFeasible)
                return true;

            if (firstSolution.IsFeasible)
                return OptimizationType == OptimizationType.Minimization
                    ? _objectiveAggr.Value(firstSolution) < _objectiveAggr.Value(secondSolution)
                    : _objectiveAggr.Value(firstSolution) > _objectiveAggr.Value(secondSolution);
            if (!secondSolution.IsFeasible)
                return _constraintAggr.Penalty(firstSolution) < _constraintAggr.Penalty(secondSolution);

            return false;
        }
    }
}