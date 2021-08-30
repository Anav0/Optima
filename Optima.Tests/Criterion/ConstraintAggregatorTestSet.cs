using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Optima.Base;
using Optima.Criteria.Constraint;

namespace Optima.Tests.Criterion
{
    internal class ConstraintAggregatorTestSet : IEnumerable<object[]>
    {
        private readonly List<object[]> _testGroups = new();

        public ConstraintAggregatorTestSet()
        {
            double[][] arrayPenalties = {new[] {1.5d, 4d}, new[] {2d, 0d}};

            foreach (var penalties in arrayPenalties)
            {
                var constraints = new Constraint<Solution>[penalties.Length];

                for (var i = 0; i < penalties.Length; i++)
                {
                    var c = new Mock<Constraint<Solution>>();
                    c.Setup(s => s.Penalty(It.IsAny<Solution>())).Returns(penalties[i]);
                    constraints[i] = c.Object;
                }

                _testGroups.Add(new object[]
                {
                    new ConstraintTestGroup
                    {
                        Aggregator = new WeightedConstraintAggregator<Solution>(constraints),
                        ExpectedPenalty = penalties.Sum(),
                    },
                });
            }
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _testGroups.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}