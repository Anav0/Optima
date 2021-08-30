using Optima.Misc;

namespace Optima.Algorithms.SimulatedAnnealing
{
    public abstract class Cooler : IResetToInitial
    {
        public virtual double Temperature { get; set; }
        public virtual double InitialTemperature { get; set; }

        public Cooler()
        {
        }

        public Cooler(double temperature)
        {
            Temperature = temperature;
            InitialTemperature = temperature;
        }

        public void ResetToInitialState()
        {
            Temperature = InitialTemperature;
        }

        public abstract double Cool();
    }
}