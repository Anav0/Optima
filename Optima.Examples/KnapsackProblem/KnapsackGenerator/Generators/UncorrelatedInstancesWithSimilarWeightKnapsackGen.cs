using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public class UncorrelatedInstancesWithSimilarWeightKnapsackGen : KnapsackInstanceGen
    {
        public UncorrelatedInstancesWithSimilarWeightKnapsackGen(byte coefficient = 3) : base(coefficient)
        {
        }

        protected override KnapsackInstance Fill(uint n)
        {
            for (int i = 0; i < n; i++)
            {
                Instance.W[i] = _rnd.Next(100000, 100100);
                Instance.V[i] = _rnd.Next(1, 1000);
            }

            return Instance;
        }
    }
}
