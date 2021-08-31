using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public class SubsetSumKnapsackGen : KnapsackInstanceGen
    {
        public SubsetSumKnapsackGen(byte coefficient = 3) : base(coefficient)
        {
        }
        protected override KnapsackInstance Fill(uint n)
        {
            for (int i = 0; i < n; i++)
            {
                Instance.W[i] = _rnd.Next(1, R);
                Instance.V[i] = Instance.W[i];
            }

            return Instance;
        }
    }
}