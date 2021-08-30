using Moq;
using Optima.Base;
using Optima.StopCriteria;
using Xunit;

namespace Optima.Tests.StopCriteria
{
    public class MaxStepsTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(150)]
        private void ShouldStop_Stops_At_MaxSteps(uint max)
        {
            var criteria = new MaxSteps<Solution>(max);
            var mockedSolution = new Mock<Solution>();

            for (var i = 0; i < max; i++)
                Assert.False(criteria.ShouldStop(mockedSolution.Object));

            Assert.True(criteria.ShouldStop(mockedSolution.Object));
        }

        [Fact]
        private void ResetToInitialState_Resest()
        {
            var mocked = new Mock<MaxSteps<Solution>>();
            mocked.SetupProperty<uint>(s => s.Steps, 100);

            mocked.Object.ResetToInitialState();

            Assert.Equal(0u, mocked.Object.Steps);
        }
    }
}