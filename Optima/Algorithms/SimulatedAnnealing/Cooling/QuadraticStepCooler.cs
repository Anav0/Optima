namespace Optima.Algorithms.SimulatedAnnealing
{
    public class QuadraticStepCooler : Cooler
    {
        private readonly double _multiplier;
        private readonly int _n;
        private int _i = 1;

        public QuadraticStepCooler(double startingTemperature, int multiplier, int n) : base(startingTemperature)
        {
            _multiplier = multiplier;
            _n = n;
        }

        public override double Cool()
        {
            if (_i == _n)
            {
                Temperature *= _multiplier;
                _i = 1;
            }
            else
            {
                _i++;
            }

            return Temperature;
        }
    }
}