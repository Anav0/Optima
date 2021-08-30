namespace Optima.Algorithms.SimulatedAnnealing
{
    public class QuadraticCooler : Cooler
    {
        private readonly double _multiplier;

        public QuadraticCooler(double multiplier, double temperature) : base(temperature)
        {
            _multiplier = multiplier;
        }

        public override double Cool()
        {
            return Temperature *= _multiplier;
        }
    }
}