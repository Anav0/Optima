using Optima.Criteria.Objective;

namespace Optima.Tests
{
    public class TestObjective<T> : Objective<T> where T : TestSolution
    {
        private readonly double _mockedValue;

        public TestObjective(double mockedValue)
        {
            _mockedValue = mockedValue;
        }

        public override double CalculateValue(T solution)
        {
            return _mockedValue;
        }
    }
}