using System;
using System.Collections.Generic;
using System.Linq;

namespace AI.Practice1
{
    class Program
    {
        // Total number of weights for the neuron. w0, w1 and w2 - 3 total weights
        public const int WEIGHT_COUNT = 3;

        // All of the provided inputs and expected classes are defined by _inputX and _expectedX variables
        private static readonly float[] _input1 = new float[] { 1f, -0.3f, 0.6f };
        private static readonly float[] _input2 = new float[] { 1f, 0.3f, -0.6f };
        private static readonly float[] _input3 = new float[] { 1f, 1.2f, -1.2f };
        private static readonly float[] _input4 = new float[] { 1f, 1.2f, 1.2f };

        private const float _expected1 = 0f;
        private const float _expected2 = 0f;
        private const float _expected3 = 1f;
        private const float _expected4 = 1f;

        static void Main()
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

            // This lambda encapsulates all of the required constraints for a valid weight set
            bool weightPredicate(float[] weights) =>
                MathF.Round(neuron.Compute(_input1, weights)) == _expected1 &&
                MathF.Round(neuron.Compute(_input2, weights)) == _expected2 &&
                MathF.Round(neuron.Compute(_input3, weights)) == _expected3 &&
                MathF.Round(neuron.Compute(_input4, weights)) == _expected4;

            // Get full list of "valid" weights within a provided interval and step size
            List<float[]> weightsList = GetAllWeightsBruteForce(weightPredicate, WEIGHT_COUNT, -5f, 5f, 0.2f);

            // Output each weight set
            foreach (var weights in weightsList)
            {
                foreach (var weight in weights)
                {
                    Console.Write(weight + " ");
                }
                Console.WriteLine();
            }
        }

        /// <param name="weightPredicate">The predicate that a weight set must match</param>
        /// <param name="weightCount">Total number of weights</param>
        /// <param name="minWeight">Minimum weight value</param>
        /// <param name="maxWeight">Maximum weight value</param>
        /// <param name="step">Step size to be taken while brute-forcing</param>
        /// <returns>List of weight sets that match the provided weightPredicate</returns>
        static List<float[]> GetAllWeightsBruteForce(Func<float[], bool> weightPredicate, int weightCount, float minWeight, float maxWeight, float step)
        {
            float[] weights = new float[weightCount];
            for (int i = 0; i < weights.Length; ++i) weights[i] = minWeight;

            List<float[]> weightList = new();

            while (weights != null)
            {
                if (weightPredicate(weights)) weightList.Add(weights.ToArray());
                weights = GetNextWeights(weights, minWeight, maxWeight, step);
            }
            return weightList;
        }

        /// <returns>Next weight set to be checked or null if last weight has been reached</returns>
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
