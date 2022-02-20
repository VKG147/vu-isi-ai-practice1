using System;

namespace AI.Practice1
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Parameter type</typeparam>
    /// <typeparam name="U">Output type</typeparam>
    public class Neuron<T, U> where T : notnull
    {
        protected readonly Func<T[], U> _actFunc;
        protected T[] _weights;
        protected readonly int _inputCount;

        public T[] Weights 
        {
            get { return _weights; }
            set 
            { 
                if (value.Length != _inputCount)
                    throw new Exception($"{nameof(value)} length must match input count.");
                _weights = value;
            }
        }

        /// <summary>
        /// Note: weights length must match inputCount
        /// </summary>
        /// <param name="actFunc">Activation function</param>
        /// <param name="weights">Initial weights</param>
        /// <param name="inputCount">Number of inputs</param>
        public Neuron(Func<T[], U> actFunc, T[] weights, int inputCount)
        {
            if (weights.Length != inputCount) 
                throw new Exception($"{nameof(weights)} length must match input count.");
            _actFunc = actFunc;
            _weights = weights;
            _inputCount = inputCount;
        }

        public U Compute(T[] input)
        {
            if (input.Length != _inputCount)
                throw new Exception($"{nameof(input)} length must match input count.");
            return _actFunc(input);
        }
    }
}
