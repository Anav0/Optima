using Optima.Criteria.Constraint;

namespace Optima.Examples.KnapsackProblem
{
    public class KnapsackConstraint : Constraint<KnapsackSolution>
    {
        private readonly KnapsackInstance _instance;

        public KnapsackConstraint(KnapsackInstance instance)
        {
            _instance = instance;
        }

        public override double Penalty(KnapsackSolution solution)
        {
            var totalWeight = 0;
            for (int i = 0; i < solution.PickedItems.Length; i++)
            {
                if (!solution.PickedItems[i]) continue;

                totalWeight += _instance.W[i];
            }

            return totalWeight > _instance.B ? totalWeight - _instance.B : 0d;
        }

        public override double Initial(KnapsackSolution solution)
        {
            return Penalty(solution);
        }
    }
}