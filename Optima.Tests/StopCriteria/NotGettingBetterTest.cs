using Moq;
using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;
using Optima.StopCriteria;
using Xunit;

namespace Optima.Tests.StopCriteria
{
    public class NotGettingBetterTest
    {
        private readonly Mock<Cooler> _cooler;
        private readonly Mock<Solution> _mockedSolution;

        private readonly uint _notGettingBetterDur;

        private NotGettingBetter<Solution> _stop;

        public NotGettingBetterTest()
        {
            _notGettingBetterDur = 300u;
            _cooler = new Mock<Cooler>();
            _cooler.SetupProperty(c => c.Temperature, 1000);
            _stop = new NotGettingBetter<Solution>(_cooler.Object, OptimizationType.Maximization, 200000, _notGettingBetterDur);

            _mockedSolution = new Mock<Solution>();
            _mockedSolution.SetupProperty(s => s.CalculatedValue, 0);
        }

        [Fact]
        private void ShouldStop_IfValueIsNotBetter()
        {
            _stop = new NotGettingBetter<Solution>(_cooler.Object, OptimizationType.Maximization, 5000, _notGettingBetterDur);
            var step = 0u;
            while (!_stop.ShouldStop(_mockedSolution.Object))
                step++;

            Assert.Equal(_notGettingBetterDur + 1, step);
        }

        [Theory]
        [InlineData(121)]
        [InlineData(100)]
        [InlineData(20)]
        [InlineData(10)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(0)]
        private void ShouldNotStop_IfValueIsBetter(uint threshold)
        {
            _stop = new NotGettingBetter<Solution>(_cooler.Object, OptimizationType.Maximization, 20000, _notGettingBetterDur);
            var step = 0u;
            while (!_stop.ShouldStop(_mockedSolution.Object))
            {
                step++;
                if (step == 10) _mockedSolution.Object.CalculatedValue += threshold + 1;
            }

            Assert.NotEqual(_notGettingBetterDur + 1, step);
        }

        [Theory]
        [InlineData(121)]
        [InlineData(100)]
        [InlineData(20)]
        [InlineData(10)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(0)]
        private void ShouldStop_IfValueIsBetterOnlyOnce(uint threshold)
        {
            _stop = new NotGettingBetter<Solution>(_cooler.Object, OptimizationType.Maximization, 5000, _notGettingBetterDur);
            var step = 0u;
            while (!_stop.ShouldStop(_mockedSolution.Object))
            {
                step++;
                if (step == 10) _mockedSolution.Object.CalculatedValue += threshold + 1;
                if (step == 100) _mockedSolution.Object.CalculatedValue += threshold + 1;
            }

            Assert.Equal(_notGettingBetterDur + 1 + 100, step);
        }

        [Theory]
        [InlineData(200u)]
        [InlineData(10u)]
        [InlineData(20000u)]
        private void ShouldStop_Max_steps(uint maxSteps)
        {
            _stop = new NotGettingBetter<Solution>(_cooler.Object, OptimizationType.Maximization, maxSteps, 250000);
            var step = 0u;
            while (!_stop.ShouldStop(_mockedSolution.Object))
                step++;

            Assert.Equal(maxSteps, step);
        }
    }
}