
namespace NeuralNetwork.ActivationFunctions
{
    public class ReluActivationFunction : IActivationFunction
    {
        public float Calculate(float value)
        {
            if (value >= 0)
                return value;
            else return 0;
        }

        public float CalculateDerivative(float value)
        {
            if (value >= 0)
                return 1;
            else return 0;
        }
    }
}
