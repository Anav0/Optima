using Optima.Algorithms.SimulatedAnnealing;
using Xunit;

namespace Optima.Tests.Coolers
{
    public class LinearStepCoolerTests
    {
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(1)]
        private void ChangeTemperature_every_n_steps(int n)
        {
            var startTemp = 2000;
            var tempChanger = new LinearStepCooler(startTemp, 10, n);
            var prevTemp = tempChanger.Temperature;
            var i = 1;
            while (tempChanger.Temperature > 5)
            {
                var temp = tempChanger.Cool();
                if (i == n)
                {
                    Assert.NotEqual(prevTemp, temp);
                    i = 1;
                }
                else
                {
                    Assert.Equal(prevTemp, temp);
                    i++;
                }

                prevTemp = temp;
            }
        }
    }
}