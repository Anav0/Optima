using System;
using Moq;
using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;
using Optima.StopCriteria;
using Xunit;

namespace Optima.Tests.StopCriteria
{
    public class UntilFrozenTests
    {
        [Fact]
        private void ShouldStop_AfterReachingMinTemp()
        {
            var mockedTempChange = new Mock<Cooler>();
            var criteria = new UntilFrozen<Solution>(mockedTempChange.Object);
            var mockedSolution = new Mock<Solution>();
            Assert.True(criteria.ShouldStop(mockedSolution.Object));
        }

        [Fact]
        private void ShouldNotStop_BeforeReachingMinTemp()
        {
            var mockedTempChange = new Mock<Cooler>();
            mockedTempChange.SetupProperty(s => s.Temperature, 2);
            var criteria = new UntilFrozen<Solution>(mockedTempChange.Object);
            var mockedSolution = new Mock<Solution>();

            while (!criteria.ShouldStop(mockedSolution.Object))
                mockedTempChange.Object.Temperature *= 0.5;

            Assert.True(mockedTempChange.Object.Temperature <= UntilFrozen<Solution>.MIN_TEMP);
        }

        [Fact]
        private void Ctor_RequireParameter()
        {
            Assert.Throws<ArgumentNullException>(() => new UntilFrozen<Solution>(null));
        }
    }
}