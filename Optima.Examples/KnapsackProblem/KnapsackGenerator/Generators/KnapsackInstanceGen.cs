using System;
using Optima.Examples.KnapsackProblem;

namespace Optima.Examples.KnapsackGenerator
{
    public abstract class KnapsackInstanceGen
    {
        protected KnapsackInstance Instance;
        protected Random _rnd = new();

        public int R { get; private set; } = 10;

        public void ChangeCoefficient(byte coefficient)
        {
            R = (int)Math.Pow(10, coefficient);
        }

        protected KnapsackInstanceGen(byte coefficient = 3)
        {
            ChangeCoefficient(coefficient);
        }

        public KnapsackInstance Generate(uint n, uint B, string name)
        {
            Instance = new KnapsackInstance(n, B, name);
            return Fill(n);
        }

        protected abstract KnapsackInstance Fill(uint n);
    }
}