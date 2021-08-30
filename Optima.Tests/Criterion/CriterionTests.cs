using Moq;
using Optima.Base;
using Optima.Criteria;
using Optima.Criteria.Constraint;
using Optima.Criteria.Objective;
using Xunit;

namespace Optima.Tests.Criterion
{
    public class CriterionTests
    {
        private readonly Criterion<Solution> _criterion;

        public CriterionTests()
        {
            var mockedConstraintAggr = new Mock<ConstraintAggregator<Solution>>();
            var mockedObjectiveAggr = new Mock<ObjectiveAggregator<Solution>>();

            _criterion = new Criterion<Solution>(OptimizationType.Minimization,
                mockedObjectiveAggr.Object,
                mockedConstraintAggr.Object);
        }

        [Fact]
        private void IsFirstOneBetter_FirstShouldBeBetter_BetterValue_BothFeasible()
        {
            var first = new Mock<Solution>();
            first.CallBase = true;
            first.SetupProperty(s => s.CalculatedValue, 0);
            first.SetupProperty(s => s.CalculatedPenalty, 0);
            first.SetupProperty(s => s.PenaltyWasCalculated, true);
            first.SetupProperty(s => s.ValueWasCalculated, true);
            first.SetupProperty(s => s.IsFeasible, true);

            var second = new Mock<Solution>();
            second.CallBase = true;
            second.SetupProperty(s => s.CalculatedValue, 10);
            second.SetupProperty(s => s.CalculatedPenalty, 0);
            second.SetupProperty(s => s.PenaltyWasCalculated, true);
            second.SetupProperty(s => s.ValueWasCalculated, true);
            second.SetupProperty(s => s.IsFeasible, true);

            var result = _criterion.IsFirstOneBetter(first.Object, second.Object);
            Assert.True(result);
        }

        [Fact]
        private void IsFirstOneBetter_FirstShouldBeBetter_SecondIsNotFeasible()
        {
            var first = new Mock<Solution>();
            first.CallBase = true;
            first.SetupProperty(s => s.CalculatedPenalty, 0);
            first.SetupProperty(s => s.PenaltyWasCalculated, true);

            var second = new Mock<Solution>();
            second.CallBase = true;
            second.SetupProperty(s => s.CalculatedPenalty, 1);
            second.SetupProperty(s => s.PenaltyWasCalculated, true);

            var result = _criterion.IsFirstOneBetter(first.Object, second.Object);
            Assert.True(result);
        }

        [Fact]
        private void IsFirstOneBetter_FirstShouldBeBetter_SecondIsNull()
        {
            var first = new Mock<Solution>();
            var result = _criterion.IsFirstOneBetter(first.Object, null);
            Assert.True(result);
        }

        [Fact]
        private void IsFirstOneBetter_FirstShouldBeBetter_LowerPenalty()
        {
            var first = new Mock<Solution>();
            first.SetupProperty(s => s.CalculatedPenalty, 1);
            first.SetupProperty(s => s.PenaltyWasCalculated, true);

            var second = new Mock<Solution>();
            second.SetupProperty(s => s.CalculatedPenalty, 3);
            second.SetupProperty(s => s.PenaltyWasCalculated, true);

            var result = _criterion.IsFirstOneBetter(first.Object, second.Object);
            Assert.True(result);
        }

        [Fact]
        private void IsFirstOneBetter_FirstIsNotBetter_When_Unfeasible()
        {
            var first = new Mock<Solution>();
            first.SetupProperty(s => s.CalculatedPenalty, 1);

            var second = new Mock<Solution>();

            var result = _criterion.IsFirstOneBetter(first.Object, second.Object);
            Assert.False(result);
        }
    }
}