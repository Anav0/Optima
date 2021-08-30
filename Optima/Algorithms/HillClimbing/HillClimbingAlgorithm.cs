using System;
using Microsoft.Extensions.Logging;
using Optima.Analysis;
using Optima.Base;
using Optima.Criteria;
using Optima.Moves;
using Optima.StopCriteria;

namespace Optima.Algorithms.HillClimbing
{
    public class HillClimbingAlgorithm<T> : OptimizationAlgorithm<T> where T : Solution
    {
        private readonly SolutionSaver _saver;

        public Mover Mover { get; }
        public IStopCriteria<T> StopCriteria { get; }

        public HillClimbingAlgorithm(string name, IStopCriteria<T> stopCriteria, Mover mover,
            ILogger<HillClimbingAlgorithm<T>> logger, SolutionSaver analyzer = null) :
            base(name, logger)
        {
            StopCriteria = stopCriteria;
            Mover = mover;
            _saver = analyzer;
        }

        public override T Solve(T solution, Criterion<T> criterion)
        {
            ResetToInitialState();

            var iter = 0;
            solution.Reset();
            _saver?.Open();

            criterion.InitialEvaluation(solution);

            var bestSol = (T) solution.Clone();
            var solBeforeMove = (T) solution.Clone();

            while (!StopCriteria.ShouldStop(solution))
            {
                solution.Copy(solBeforeMove);
                Mover.MoveSolution(solution);

                criterion.CalculateSolutionPenalty(solution);

                if (IsMovedBetter(solution, solBeforeMove, criterion))
                {
                    if (criterion.IsFirstOneBetter(solution, bestSol))
                        solution.Copy(bestSol);
                }
                else
                {
                    solBeforeMove.Copy(solution);
                }

                AtEndOfIteration(iter);
                _saver?.AppendToFile(bestSol);
                iter++;
            }

            Logger.LogInformation($"Finished in: '{iter}' iterations");
            _saver?.Close();

            if (!bestSol.IsFeasible) throw new Exception("No feasible solution was found");
            solution = bestSol;
            return solution;
        }

        private bool IsMovedBetter(T movedSol, T solBeforeMove, Criterion<T> criterion)
        {
            if (criterion.IsFirstOneBetter(movedSol, solBeforeMove))
                return true;

            double movedSolValue, unmovedSolValue;

            if (movedSol.IsFeasible)
            {
                movedSolValue = criterion.CalculateSolutionValue(movedSol);
                unmovedSolValue = criterion.CalculateSolutionValue(solBeforeMove);
            }
            else
            {
                movedSolValue = movedSol.CalculatedPenalty;
                unmovedSolValue = solBeforeMove.CalculatedPenalty;
            }

            var diff =
                criterion.OptimizationType == OptimizationType.Minimization
                    ? unmovedSolValue - movedSolValue
                    : movedSolValue - unmovedSolValue;

            if (diff == 0)
                return false;

            return diff > 0 || AtEndOfIsMovedBetter(diff);
        }

        public override void ResetToInitialState()
        {
            Mover.ResetToInitialState();
            StopCriteria.ResetToInitialState();
        }

        protected virtual void AtEndOfIteration(int iter)
        {
        }

        protected virtual bool AtEndOfIsMovedBetter(double diff)
        {
            return false;
        }
    }
}