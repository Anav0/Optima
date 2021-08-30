using Optima.Criteria.Objective;

namespace Optima.Examples.KnapsackProblem
{
    public class KnapsackObj : Objective<KnapsackSolution>
    {
        private KnapsackInstance _instance;

        public KnapsackObj(KnapsackInstance instance)
        {
            _instance = instance;
        }

        public override double CalculateValue(KnapsackSolution solution)
        {
            var totalValue = 0;

            for (int i = 0; i < solution.PickedItems.Length; i++)
            {
                if (!solution.PickedItems[i]) continue;
                totalValue += _instance.V[i];
            }

            return totalValue;
        }
    }
}