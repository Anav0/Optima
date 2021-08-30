using Optima.Base;
using Optima.Criteria.Constraint;

namespace Optima.Tests
{
    internal struct ConstraintTestGroup
    {
        public ConstraintAggregator<Solution> Aggregator;
        public double ExpectedPenalty;

        public override string ToString()
        {
            return $"{Aggregator} {ExpectedPenalty}";
        }
    }
}