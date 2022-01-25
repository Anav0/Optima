using System.Xml;
using Microsoft.Extensions.Logging;
using Optima.Base;
using Optima.Criteria;

namespace Optima.Algorithms.ParticalSwarm
{
    public class ParticleSwarm<T> : OptimizationAlgorithm<T> where T : Solution
    {
        private readonly Particle[] _particles;

        public ParticleSwarm(string name, ILogger<OptimizationAlgorithm<T>> logger) : base(name, logger)
        {
        }

        public override void ResetToInitialState()
        {
            throw new System.NotImplementedException();
        }

        public override T Solve(T solution, Criterion<T> criterion)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Particle
    {
        /// <summary>
        /// Particle velocity
        /// </summary>
        public int V;

        public float Inertia;

        /// <summary>
        /// Value from 0 to 1 indication preference betwwen local and global solution.
        /// 0 prefers only global
        /// 1 prefers only local
        /// </summary>
        public float PromoteLocalSolution;

        /// <summary>
        /// Best local solution found by this particle
        /// </summary>
        public Solution BestLocal;
        
        /// <summary>
        /// Best global solution found by whole swarm
        /// </summary>
        public Solution BestGlobal;
    }
}