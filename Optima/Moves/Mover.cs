using Optima.Base;
using Optima.Misc;

namespace Optima.Moves
{
    public abstract class Mover : IResetToInitial
    {
        public abstract void ResetToInitialState();
        protected abstract void Move(Solution solution);

        public void MoveSolution(Solution solution)
        {
            Move(solution);

            // solution.CalculatedPenalty = 0;
            // solution.CalculatedValue = 0;
            solution.IsFeasible = false;
            solution.ValueWasCalculated = false;
            solution.PenaltyWasCalculated = false;
        }
    }
}