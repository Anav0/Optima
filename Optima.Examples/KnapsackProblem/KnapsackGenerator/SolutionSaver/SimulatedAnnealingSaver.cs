using Optima.Base;

namespace Optima.Examples.KnapsackGenerator
{
    public class SimulatedAnnealingSaver : SolutionSaver
    {
        public int Iter;
        public double Temp;

        public SimulatedAnnealingSaver(string filename, bool append = false, string appendToHeader = "") : base(filename, append, appendToHeader)
        {
        }

        protected override string GetHeader() => "Iter,Temp,Value";
        protected override string SolutionAsCsvRow(Solution solution) => $"{Iter},{Temp},{solution.CalculatedValue}";
    }
}