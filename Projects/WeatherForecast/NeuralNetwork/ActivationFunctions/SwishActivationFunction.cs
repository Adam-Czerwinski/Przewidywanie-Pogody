
namespace NeuralNetwork.ActivationFunctions
{
    public class SwishActivationFunction :IActivationFunction
    {
        private SigmoidActivationFunction sigmoid = new SigmoidActivationFunction();
        public float Calculate(float value)
        {
            return sigmoid.Calculate(value) * value;
        }

        public float CalculateDerivative(float value)
        {
            var swish = Calculate(value);
            return swish + sigmoid.Calculate(value) * (1 - swish);
        }
    }
}
