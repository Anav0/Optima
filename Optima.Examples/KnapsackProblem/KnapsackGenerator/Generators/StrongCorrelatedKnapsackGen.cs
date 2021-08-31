using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public class StrongCorrelatedKnapsackGen : KnapsackInstanceGen
    {
        public StrongCorrelatedKnapsackGen(byte coefficient = 3) : base(coefficient)
        {
        }

        protected override KnapsackInstance Fill(uint n)
        {
            for (int i = 0; i < n; i++)
            {
                Instance.W[i] = _rnd.Next(1, R);
                Instance.V[i] = Instance.W[i] + R / 10;
            }

            return Instance;
        }
    }
}
