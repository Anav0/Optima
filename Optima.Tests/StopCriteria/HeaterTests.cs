using Moq;
using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;
using Optima.StopCriteria;
using Xunit;

namespace Optima.Tests.StopCriteria
{
    public class HeaterTests
    {
        private readonly Mock<Cooler> _cooler;
        private readonly Mock<Solution> _mockedSolution;

        private Heater<Solution> _stopCriteria;

        public HeaterTests()
        {
            _cooler = new Mock<Cooler>();
            _cooler.SetupProperty(c => c.Temperature, 1000);
            _stopCriteria = new Heater<Solution>(_cooler.Object, OptimizationType.Maximization, 50000, 0.5f, 5);

            _mockedSolution = new Mock<Solution>();
            _mockedSolution.SetupProperty(s => s.CalculatedValue, 0);
        }

        [Theory]
        [InlineData(200u)]
        [InlineData(10u)]
        [InlineData(20000u)]
        private void ShouldStop_Max_steps(uint maxSteps)
        {
            _stopCriteria = new Heater<Solution>(_cooler.Object, OptimizationType.Maximization, maxSteps, 1000, 5);
            var step = 0u;
            while (!_stopCriteria.ShouldStop(_mockedSolution.Object))
                step++;

            Assert.Equal(maxSteps, step);
        }
    }
}