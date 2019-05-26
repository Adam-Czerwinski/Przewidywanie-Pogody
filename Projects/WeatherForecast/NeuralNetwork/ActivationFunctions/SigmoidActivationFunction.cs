
using System;

namespace NeuralNetwork.ActivationFunctions
{
    public class SigmoidActivationFunction : IActivationFunction
    {
        public float Calculate(float value)
        {
            return 1.0f / (1.0f + (float)Math.Exp(-value));
        }

        public float CalculateDerivative(float value)
        {
            var val = Calculate(value);
            return val * (1 - val);
        }
    }
}
