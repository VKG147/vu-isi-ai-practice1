using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Practice1
{
    public class SigmoidNeuron : Neuron<double, double>
    {
        private static double SigmoidFunc(double input)
        {
            return 1/(1+Math.Pow(Math.E, input*(-1)))
        }

        public SigmoidNeuron(double[] weights, int inputCount) : base()
        {
            
        }
    }
}
