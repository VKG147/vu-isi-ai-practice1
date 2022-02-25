namespace AI.Practice1
{
    public class BinaryStopNeuron : Neuron<float>
    {
        private static float BinaryStopFunc(float a)
        {
            return a >= 0 ? 1 : 0;
        }

        public BinaryStopNeuron(float[] weights) : base(BinaryStopFunc, weights) { }
        public BinaryStopNeuron(int weightCount) : base(BinaryStopFunc, weightCount) { }
    }
}
