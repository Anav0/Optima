using Xunit;

namespace Optima.Tests.Criterion
{
    public class ConstraintAggregatorTests
    {
        [Theory]
        [ClassData(typeof(ConstraintAggregatorTestSet))]
        private void Penalty_Works(ConstraintTestGroup group)
        {
            var sol = new TestSolution();
            Assert.Equal(group.ExpectedPenalty, group.Aggregator.Penalty(sol));
            Assert.True(sol.PenaltyWasCalculated);
            Assert.Equal(group.ExpectedPenalty, sol.CalculatedPenalty);
        }
    }
}