using Optima.Base;

namespace Optima.Analysis
{
    public class SimulatedAnnealingSaver : SolutionSaver
    {
        private int _iter;
        private double _temp;

        public SimulatedAnnealingSaver(string folderPath, string filename, bool append = false, string appendToHeader = "") : base(folderPath,
            filename, append, appendToHeader)
        {
        }

        public void ChangeParams(int iter, double temp)
        {
            _iter = iter;
            _temp = temp;
        }

        protected override string GetHeader()
        {
            return "Iter,Temp,Penalty,Value";
        }

        protected override string SolutionAsCsvRow(Solution solution)
        {
            return $"{_iter},{_temp},{solution.CalculatedPenalty},{solution.CalculatedValue}";
        }
    }
}