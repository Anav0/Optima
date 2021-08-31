using System;
using Microsoft.Extensions.Logging;
using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;
using Optima.Criteria;
using Optima.Criteria.Constraint;
using Optima.Criteria.Objective;
using Optima.Examples.KnapsackGenerator;
using Optima.Examples.KnapsackProblem;
using Optima.StopCriteria;

namespace Optima.Examples
{
    public static partial class ExamplesRunner
    {
        public static void KnapsackTest()
        {
            Console.WriteLine("============");
            Console.WriteLine("SA Knapsack problem example");
            Console.WriteLine("Knapsack instances are generated according to this paper:");
            Console.WriteLine("http://www.dcs.gla.ac.uk/~pat/cpM/jchoco/knapsack/papers/hardInstances.pdf");
            Console.WriteLine("============");
            var logger = new Logger<SimulatedAnnealing<KnapsackSolution>>(new LoggerFactory());
            var cooler = new QuadraticCooler(0.998, 800);
            var mover = new KnapsackMover();

            var instances = new KnapsackInstancesFactory(50, 5000, 1)
                            .GenDistribution(DistributionVariant.Strong, 3)
                            .GenDistribution(DistributionVariant.Uncorrelated, 3)
                            .Collect();

            var stop = new NotGettingBetter<KnapsackSolution>(cooler, OptimizationType.Maximization, 500000, 300000);
            Console.WriteLine("COMMON ELEMENTS");
            Console.WriteLine($"    Stop criteria: {stop}");
            Console.WriteLine($"    Cooler: {cooler}");
            Console.WriteLine("INSTANCES");
            for (int i = 0; i < instances.Count; i++)
            {
                var instance = instances[i];
                Console.WriteLine($"Instance name: {instance.Name}");

                var sa = new SimulatedAnnealing<KnapsackSolution>(cooler, mover, stop, logger);
                var obj = new KnapsackObj(instance);
                var constraint = new KnapsackConstraint(instance);

                var objAggr = new WeightedObjectiveAggregator<KnapsackSolution>(new[] { obj }, new double[] { 1 });
                var constraintAggr = new WeightedConstraintAggregator<KnapsackSolution>(new[] { constraint }, new double[] { 1 });
                var criterion = new Criterion<KnapsackSolution>(OptimizationType.Maximization, objAggr, constraintAggr);

                var solution = new KnapsackSolution(instance);

                var bestSol = sa.Solve(solution, criterion);

                Console.WriteLine(instance);
                Console.WriteLine(bestSol);
                Console.WriteLine("----");
            }
        }
    }
}