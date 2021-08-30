namespace Optima.Algorithms.SimulatedAnnealing
{
    public class LinearStepCooler : Cooler
    {
        private readonly double _coolBy;
        private readonly int _n;
        private int _i = 1;

        public LinearStepCooler(double startingTemperature, int coolBy, int n) : base(startingTemperature)
        {
            _coolBy = coolBy;
            _n = n;
        }

        public override double Cool()
        {
            if (_i == _n)
            {
                Temperature -= _coolBy;
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