using System;

namespace AI.Practice1
{
    public class SigmoidNeuron : Neuron<float>
    {
        private static float SigmoidFunc(float a)
        {
            return 1 / (1 + MathF.Exp(-a));
        }

        public SigmoidNeuron(float[] weights) : base(SigmoidFunc, weights) { }
        public SigmoidNeuron(int weightCount) : base(SigmoidFunc, weightCount) { }
    }
}
