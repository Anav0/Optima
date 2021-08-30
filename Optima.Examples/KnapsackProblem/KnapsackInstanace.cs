using System;

namespace Optima.Examples.KnapsackProblem
{
    public class KnapsackInstance
    {
        public ushort B; //Backpack capacity;
        public ushort[] V; // Item values;
        public ushort[] W; // Item weights;

        public KnapsackInstance(ushort b, ushort[] v, ushort[] w)
        {
            if (v.Length == 0) throw new ArgumentException("Values and weights cannot be empty");
            if (v.Length != w.Length) throw new ArgumentException("Backpack weights and values need to be the same size");
            B = b;
            V = v;
            W = w;
        }

        public override string ToString()
        {
            var str = $"Capacity: {B}\nValues: [{V[0]}";
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