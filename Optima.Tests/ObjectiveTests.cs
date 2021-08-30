using Moq;
using Xunit;

namespace Optima.Tests
{
    public class ObjectiveTests
    {
        [Fact]
        private void Value_Recalculate_When_ValueWasNotCalculated()
        {
            var mockedSolution = new Mock<TestSolution>();

            mockedSolution.SetupProperty(s => s.ValueWasCalculated, true);
            mockedSolution.SetupProperty(s => s.CalculatedValue, 150);

            var mockedObjective = new Mock<TestObjective<TestSolution>>(25);
            mockedObjective.Object.CalculateValue(mockedSolution.Object);

            Assert.Equal(150, mockedSolution.Object.CalculatedValue);
        }
    }
}