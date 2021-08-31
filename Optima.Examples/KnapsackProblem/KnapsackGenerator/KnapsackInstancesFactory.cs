using System;
using System.Collections.Generic;
using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public class KnapsackInstancesFactory
    {
        private uint _howManyInstances;
        private Dictionary<DistributionVariant, KnapsackInstanceGen> _generators;
        private List<KnapsackInstance> _instances = new();
        private Random _rnd = new();

        private uint _n;
        private uint _B;

        public KnapsackInstancesFactory(uint n, uint B, uint howManyInstances)
        {
            _howManyInstances = howManyInstances;
            ChangeKnapsackParams(n, B, howManyInstances);
            _generators = new Dictionary<DistributionVariant, KnapsackInstanceGen>()
            {
                {
                    DistributionVariant.Uncorrelated, new UncorrelatedKnapsackGen()
                },
                {
                    DistributionVariant.Weak, new WeaklyCorrelatedKnapsackGen()
                },
                {
                    DistributionVariant.Strong, new StrongCorrelatedKnapsackGen()
                },
                {
                    DistributionVariant.Inverse, new InverseStrongCorrelatedKnapsackGen()
                },
                {
                    DistributionVariant.Similar, new UncorrelatedInstancesWithSimilarWeightKnapsackGen()
                },
                {
                    DistributionVariant.SubsetSum, new SubsetSumKnapsackGen()
                },
                {
                    DistributionVariant.Almost, new AlmostStrongCorrelatedKnapsackGen()
                }
            };
        }

        public KnapsackInstancesFactory ChangeKnapsackParams(uint n, uint B, uint howManyInstances)
        {
            _n = n;
            _B = B;
            _howManyInstances = howManyInstances;
            return this;
        }

        public KnapsackInstancesFactory GenDistribution(DistributionVariant variant, byte coeficient)
        {
            for (int i = 0; i < _howManyInstances; i++)
            {
                var gen = _generators[variant];
                gen.ChangeCoefficient(coeficient);
                _instances.Add(gen.Generate(_n, _B, $"{variant.ToString()}({gen.R.ToString()})"));
            }

            return this;
        }

        public KnapsackInstancesFactory GenSpanner(DistributionVariant variant, byte coeficient = 3, int v = 2, int m = 10)
        {
            for (int o = 0; o < _howManyInstances; o++)
            {
                var gen = _generators[variant];
                gen.ChangeCoefficient(coeficient);

                var instance = gen.Generate(_n, _B, $"Spanner/{variant.ToString()}({gen.R}-{v}-{m})");
                var spanerV = new ushort[v];
                var spanerW = new ushort[v];

                for (int k = 0; k < v; k++)
                {
                    spanerW[k] = (ushort)((_rnd.Next(1, gen.R) * 2) / m);
                    spanerV[k] = (ushort)((instance.V[k] * 2) / m);
                }

                for (int i = 0; i < _n; i++)
                {
                    var s = _rnd.Next(v);
                    var a = _rnd.Next(1, m);
                    instance.W[i] = (ushort)(spanerW[s] * a);
                    instance.V[i] = (ushort)(spanerV[s] * a);
                }

                _instances.Add(instance);
            }

            return this;
        }

        public KnapsackInstancesFactory GenMstr(int k1, int k2, int coeficient = 3, int d = 3)
        {
            for (int o = 0; o < _howManyInstances; o++)
            {
                //mstr(3R/10, 2R/10, 3)
                if (k1 == k2) throw new Exception("k1 and k2 need to be different");

                var R = (int)Math.Pow(10, coeficient);
                var instance = new KnapsackInstance(_n, _B, $"Mstr({k1}-{k2}-{R}-{d})");
                for (int i = 0; i < _n; i++)
                {
                    instance.W[i] = (ushort)_rnd.Next(1, R);
                    if (instance.W[i] % d == 0)
                        instance.V[i] = (ushort)(instance.W[i] + k1);
                    else
                        instance.V[i] = (ushort)(instance.W[i] + k2);
                }

                _instances.Add(instance);
            }

            return this;
        }

        public KnapsackInstancesFactory GenPceil(int coeficient = 3, int d = 3)
        {
            var R = (int)Math.Pow(10, coeficient);
            var instance = new KnapsackInstance(_n, _B, $"Pceail({R}-{d})");
            for (int i = 0; i < _n; i++)
            {
                instance.W[i] = _rnd.Next(1, R);
                instance.V[i] = d * Math.Abs(instance.W[i] / d);
            }

            _instances.Add(instance);
            return this;
        }


        public KnapsackInstancesFactory GenCircle(int coeficient = 3, double d = 0.75)
        {
            var R = (int)Math.Pow(10, coeficient);
            var instance = new KnapsackInstance(_n, _B, $"Circle({R}-{d})");
            for (int i = 0; i < _n; i++)
            {
                instance.W[i] = _rnd.Next(1, R);
                instance.V[i] = d * Math.Sqrt(4 * (R ^ 2) - ((int)(instance.W[i] - 2 * R) ^ 2));
            }

            _instances.Add(instance);
            return this;
        }

        public List<KnapsackInstance> Collect()
        {
            var tmp = new List<KnapsackInstance>(_instances);
            _instances.Clear();
            return tmp;
        }
    }
}