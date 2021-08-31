using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public class WeaklyCorrelatedKnapsackGen : KnapsackInstanceGen
    {
        public WeaklyCorrelatedKnapsackGen(byte coefficient = 3) : base(coefficient)
        {
        }

        protected override KnapsackInstance Fill(uint n)
        {
            for (int i = 0; i < n; i++)
            {
                var x = _rnd.Next(1, R);
                Instance.W[i] = x;
                var min = x - R / 10;
                var max = x + R / 10;
                Instance.V[i] = _rnd.Next(min, max);
            }

            return Instance;
        }
    }
}
