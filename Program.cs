using System;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AI.Practice1
{
    class Program
    {
        public const int WEIGHT_COUNT = 3;
        private static readonly float[] _input1 = new float[] { 1f, -0.3f, 0.6f };
        private static readonly float[] _input2 = new float[] { 1f, 0.3f, -0.6f };
        private static readonly float[] _input3 = new float[] { 1f, 1.2f, -1.2f };
        private static readonly float[] _input4 = new float[] { 1f, 1.2f, 1.2f };

        private const float _expected1 = 0f;
        private const float _expected2 = 0f;
        private const float _expected3 = 1f;
        private const float _expected4 = 1f;

        static void Main(string[] args)
        {
            Console.WriteLine("Please choose an activation function for the artificial neuron:");
            Console.WriteLine("a - sigmoid function");
            Console.WriteLine("b - binary stop function");

            Neuron<float> neuron = null;
            do
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "a") 
                    neuron = new SigmoidNeuron(WEIGHT_COUNT);
                else if (input.ToLower() == "b") 
                    neuron = new BinaryStopNeuron(WEIGHT_COUNT);
            }
            while (neuron == null);

            Func<float[], bool> weightPredicate = weights =>
                MathF.Round(neuron.Compute(_input1, weights)) == _expected1 &&
                MathF.Round(neuron.Compute(_input2, weights)) == _expected2 &&
                MathF.Round(neuron.Compute(_input3, weights)) == _expected3 &&
                MathF.Round(neuron.Compute(_input4, weights)) == _expected4;

            float[] weights = GetWeightsBruteForce(weightPredicate, WEIGHT_COUNT, -5f, 5f, 0.2f);

            for (int i = 0; i < weights?.Length; ++i)
            {
                Console.Write(weights[i] + " ");
            } 
            Console.Write("\n");
        }

        static float[] GetWeightsBruteForce(
            Func<float[], bool> weightPredicate,
            int weightCount,
            float minWeight,
            float maxWeight,
            float step)
        {
            float[] weights = new float[weightCount];
            for (int i = 0; i < weights.Length; ++i) weights[i] = minWeight;

            while (weights != null)
            {
                if (weightPredicate(weights)) return weights;
                weights = GetNextWeights(weights, minWeight, maxWeight, step);
            }
            return null;
        }

        static float[] GetNextWeights(float[] weights, float minWeight, float maxWeight, float step)
        {
            for (int i = 0; i < weights.Length; ++i)
            {
                if (weights[i] + step <= maxWeight) { weights[i] += step; return weights; }
                weights[i] = minWeight;
            }
            return null;
        }
    }
}
