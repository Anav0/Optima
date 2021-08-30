using Optima.Base;

namespace Optima.Criteria.Constraint
{
    public abstract class Constraint<T> where T : Solution
    {
        public abstract double Penalty(T solution);
        public abstract double Initial(T solution);
    }
}