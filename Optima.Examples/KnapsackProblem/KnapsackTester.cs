using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Optima.Algorithms.SimulatedAnnealing;
using Optima.Base;
using Optima.Criteria;
using Optima.Criteria.Constraint;
using Optima.Criteria.Objective;
using Optima.StopCriteria;

namespace Optima.Examples.KnapsackProblem
{
    public static class KnapsackTester
    {
        public static void Test()
        {
            ushort[] v = { 10, 1, 2, 5, 1, 20 };
            ushort[] w = { 7, 2, 4, 1, 7, 20 };

            var logger = new Logger<SimulatedAnnealing<KnapsackSolution>>(new LoggerFactory());

            var instance = new KnapsackInstance(30, v, w);
            var obj = new KnapsackObj(instance);
            var constraint = new KnapsackConstraint(instance);

            var cooler = new QuadraticCooler(0.995, 500);
            var mover = new KnapsackMover();
            var stop = new NotGettingBetter<KnapsackSolution>(cooler, OptimizationType.Maximization, 50000, 5000);
            var sa = new SimulatedAnnealing<KnapsackSolution>(cooler, mover, stop, logger);

            var objAggr = new WeightedObjectiveAggregator<KnapsackSolution>(new[] { obj }, new double[] { 1 });
            var constraintAggr = new WeightedConstraintAggregator<KnapsackSolution>(new[] { constraint }, new double[] { 1 });
            var criterion = new Criterion<KnapsackSolution>(OptimizationType.Maximization, objAggr, constraintAggr);

            var pickedItems = new bool[v.Length];
            
            for (int i = 0; i < v.Length; i++)
                pickedItems[i] = true;

            var solution = new KnapsackSolution(pickedItems);

            var bestSol = sa.Solve(solution, criterion);

            Console.WriteLine(instance);
            Console.WriteLine(bestSol);
        }
    }
}