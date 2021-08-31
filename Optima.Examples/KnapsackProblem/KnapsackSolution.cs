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

        public KnapsackSolution(KnapsackInstance instance)
        {
            PickedItems = new bool[instance.V.Length];
            for (int i = 0; i < instance.V.Length; i++)
                PickedItems[i] = true;
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
            var b = PickedItems[0] ? 1 : 0;
            var str = $"Value: {CalculatedValue}\nPickedItems: [{b}";
            for (int i = 1; i < PickedItems.Length; i++)
            {
                b = PickedItems[i] ? 1 : 0;
                str += $",{b}";
            }

            str += "]";
            return str;
        }
    }
}