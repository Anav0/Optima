using Optima.Base;

namespace Optima.Tests
{
    public class TestSolution : Solution
    {
        public TestSolution()
        {
        }

        public TestSolution(double value, bool wasCalculated = true)
        {
            CalculatedValue = value;
            ValueWasCalculated = wasCalculated;
        }

        protected override void CopyTo(Solution solution)
        {
        }

        protected override void ResetToInitialState()
        {
        }

        public override Solution Clone()
        {
            return new TestSolution(CalculatedValue);
        }

        public override string ToString()
        {
            return $"Value: {CalculatedValue}";
        }
    }
}