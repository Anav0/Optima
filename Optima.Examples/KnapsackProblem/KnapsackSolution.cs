using System;
using Optima.Base;

namespace Optima.Examples.KnapsackProblem
{
    public class KnapsackSolution : Solution
    {
        public bool[] PickedItems;

        public KnapsackSolution(bool[] pickedItems)
        {
            PickedItems = pickedItems;
        }

        protected override void CopyTo(Solution solution)
        {
            var casted = (KnapsackSolution)solution;

            for (int i = 0; i < PickedItems.Length; i++)
                casted.PickedItems[i] = PickedItems[i];
        }

        protected override void ResetToInitialState()
        {
            for (int i = 0; i < PickedItems.Length; i++)
                PickedItems[i] = true;
        }

        public override Solution Clone()
        {
            return new KnapsackSolution((bool[])PickedItems.Clone())
            {
                CalculatedPenalty = CalculatedPenalty,
                CalculatedValue = CalculatedValue,
                IsFeasible = IsFeasible,
                PenaltyWasCalculated = PenaltyWasCalculated,
                ValueWasCalculated = ValueWasCalculated
            };
        }

        public override string ToString()
        {
            var str = $"PickedItems: [{PickedItems[0]}";
            for (int i = 1; i < PickedItems.Length; i++)
                str += $",{PickedItems[i]}";
            str += "]";
            return str;
        }
    }
}