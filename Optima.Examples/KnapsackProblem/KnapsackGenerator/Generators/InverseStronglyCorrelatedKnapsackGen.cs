using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public class InverseStrongCorrelatedKnapsackGen : KnapsackInstanceGen
    {
        public InverseStrongCorrelatedKnapsackGen(byte coefficient = 3) : base(coefficient)
        {
        }

        protected override KnapsackInstance Fill(uint n)
        {
            for (int i = 0; i < n; i++)
            {
                Instance.V[i] = _rnd.Next(1, R);
                Instance.W[i] = Instance.W[i] + R / 10;
            }

            return Instance;
        }
    }
}
