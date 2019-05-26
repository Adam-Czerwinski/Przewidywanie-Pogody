using NeuralNetwork.ActivationFunctions;

namespace NeuralNetwork
{
    public class ActivationFunctionClient
    {
        private IActivationFunction _activationFunction;

        public ActivationFunctionClient(IActivationFunction activationFunction)
        {
            _activationFunction = activationFunction;
        }

        public float Calculate(float value)
        {
            return _activationFunction.Calculate(value);
        }

        public float CalculateDerivative(float value)
        {
            return _activationFunction.CalculateDerivative(value);
        }

    }
}
