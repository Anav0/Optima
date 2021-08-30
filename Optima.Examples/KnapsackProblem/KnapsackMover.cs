using System;
using Optima.Base;
using Optima.Moves;

namespace Optima.Examples.KnapsackProblem
{
    public class KnapsackMover : Mover
    {
        private readonly Random _rnd = new();

        public override void ResetToInitialState()
        {
        }

        protected override void Move(Solution solution)
        {
            var casted = (KnapsackSolution)solution;
            var i = _rnd.Next(casted.PickedItems.Length);
            casted.PickedItems[i] = !casted.PickedItems[i];
        }
    }
}