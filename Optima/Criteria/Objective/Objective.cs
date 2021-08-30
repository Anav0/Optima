using Optima.Base;

namespace Optima.Criteria.Objective
{
    public abstract class Objective<T> where T : Solution
    {
        public abstract double CalculateValue(T solution);
    }
}