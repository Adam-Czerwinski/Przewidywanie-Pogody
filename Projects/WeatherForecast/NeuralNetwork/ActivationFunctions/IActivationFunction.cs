

namespace NeuralNetwork.ActivationFunctions
{
    public interface IActivationFunction
    {
        float Calculate(float value);
        float CalculateDerivative(float value);
    }
}
