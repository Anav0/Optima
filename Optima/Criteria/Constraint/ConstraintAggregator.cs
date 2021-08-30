using Optima.Base;

namespace Optima.Criteria.Constraint
{
    public abstract class ConstraintAggregator<T> where T : Solution
    {
        protected readonly Constraint<T>[] Constraints;

        public ConstraintAggregator()
        {
            //INFO: Used by Moq 
        }

        protected ConstraintAggregator(Constraint<T>[] constraints)
        {
            Constraints = constraints;
        }

        public double Penalty(T solution)
        {
            if (solution.PenaltyWasCalculated) return solution.CalculatedPenalty;

            solution.CalculatedPenalty = CalculatePenalty(solution);
            solution.PenaltyWasCalculated = true;
            solution.IsFeasible = solution.CalculatedPenalty == 0;

            return solution.CalculatedPenalty;
        }

        public double Initial(T solution)
        {
            solution.CalculatedPenalty = InitialCalculation(solution);
            solution.PenaltyWasCalculated = true;
            solution.IsFeasible = solution.CalculatedPenalty == 0;
            return solution.CalculatedPenalty;
        }

        protected abstract double CalculatePenalty(T solution);
        protected abstract double InitialCalculation(T solution);
    }
}