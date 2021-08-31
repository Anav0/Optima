using System;

namespace Optima.Examples.KnapsackProblem
{
    public class KnapsackInstance
    {
        public uint B; //Backpack capacity;
        public readonly string Name;
        public double[] V; // Item values;
        public double[] W; // Item weights;

        public KnapsackInstance(uint b, double[] v, double[] w)
        {
            if (v.Length == 0) throw new ArgumentException("Values and weights cannot be empty");
            if (v.Length != w.Length) throw new ArgumentException("Backpack weights and values need to be the same size");
            B = b;
            V = v;
            W = w;
        }

        public KnapsackInstance(uint n, uint B, string name)
        {
            this.B = B;
            Name = name;
            V = new double[n];
            W = new double[n];
        }

        public override string ToString()
        {
            var str = $"Capacity: {B}\nNumber of items: {V.Length}\nValues:  [{V[0]}";
            for (int i = 1; i < V.Length; i++)
                str += $",{V[i]}";
            str += "]\n";

            str += $"Weights: [{W[0]}";
            for (int i = 1; i < W.Length; i++)
                str += $",{W[i]}";
            str += "]";

            return str;
        }
    }
}