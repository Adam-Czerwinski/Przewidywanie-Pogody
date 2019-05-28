using System;

namespace NeuralNetwork
{
    public class Layer
    {
        /// <summary>
        /// Ilość neuronów w poprzedniej warstwie
        /// </summary>
        int numberOfInputs;
        /// <summary>
        /// Ilość neuronów w aktualnej warstwie
        /// </summary>
        int numberOfOuputs;

        /// <summary>
        /// Wartości neuronów w aktualnej warstwie
        /// </summary>
        public float[] outputs;
        /// <summary>
        /// Wartości neuronów w poprzedniej warstwie
        /// </summary>
        public float[] inputs;
        /// <summary>
        /// Wartości wag wchodzących do danego neuronu w aktualnej warstwie
        /// np.:
        /// weights[0,1] Pierwszy neuron w aktualnej warstwie, druga waga pod względem kolejności wchodząca do neurona
        /// Można też o tym pomyśleć jako
        /// np.: 
        /// weights[1,1] Drugi neuron w aktualnej warstwie, waga idzie z drugiego neuronu w poprzedniej warstwie
        /// </summary>
        public float[,] weights;
        /// <summary>
        /// Delta dla każdego neuronu w aktualnej warstwie (patrz wzory)
        /// np.:
        /// weightsDelta[1,0] Drugi neuron w aktualnej warstwie połączony z pierwszym neuronem w poprzedniej warstwie
        /// 
        /// </summary>
        public float[,] weightsDelta;
        /// <summary>
        /// Gamma w aktualnej warstwie (patrz wzory)
        /// </summary>
        public float[] gamma; //gamma of this layer
        /// <summary>
        /// Delta dla każdego neuronu wyjściowego (pochodna z MSE) 
        /// </summary>
        public float[] error;
        /// <summary>
        /// Używana do inicjalizowania wag
        /// </summary>
        public static Random random = new Random();

        //testy
        public static float maximumWeight = 0.3f;
        public static float minimumWeight = 0.02f;


        /// <summary>
        /// Aktualny Total Error (MSE)
        /// </summary>
        public float totalError { get; private set; }
        public float maxError { get; private set; } = -1000;
        public float minError { get; private set; } = 1000;

        /// <summary>
        /// Konstrukcja warstwy
        /// </summary>
        /// <param name="numberOfInputs">Liczba neuronów w poprzedniej warstwie</param>
        /// <param name="numberOfOuputs">Liczba neuronów w aktualnej warstwie</param>
        public Layer(int numberOfInputs, int numberOfOuputs)
        {
            this.numberOfInputs = numberOfInputs;
            this.numberOfOuputs = numberOfOuputs;

            outputs = new float[numberOfOuputs];
            inputs = new float[numberOfInputs];
            weights = new float[numberOfOuputs, numberOfInputs];
            weightsDelta = new float[numberOfOuputs, numberOfInputs];
            gamma = new float[numberOfOuputs];
            error = new float[numberOfOuputs];

            InitilizeWeights();
        }

        /// <summary>
        /// Inicjalizuje wagi losowo
        /// </summary>
        public void InitilizeWeights()
        {
            //pierwotny kod
            //for (int i = 0; i < numberOfOuputs; i++)
            //{
            //    for (int j = 0; j < numberOfInputs; j++)
            //    {
            //        weights[i, j] = (float)random.NextDouble() - 0.5f;
            //    }
            //}



            
            //TEST
            //Modyfikowalny zakres
            for (int i = 0; i < numberOfOuputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] = (float)random.NextDouble() * (maximumWeight - minimumWeight) + minimumWeight;
                }
            }
        }

        /// <summary>
        /// Feed Forward warstwy.
        /// Warstwa dostaje dane wejściowe, czyli dane neuronów w poprzedniej warstwie (po aktywacji)
        /// Dzięki temu może wyliczyć dla siebie swoje neurony (outputs)
        /// </summary>
        /// <param name="inputs">Wartości neuronów w poprzedniej warstwie (po aktywacji)</param>
        /// <returns>
        /// Zwraca wartości neuronów aktualnej warstwy
        /// </returns>
        public float[] FeedForward(float[] inputs)
        {
            /* Do wyliczenia nowej wagi, przy wstecznej propagacji, potrzebujemy wartości neuronu po aktywacji
             * Czyli wartość neuronu z którego wychodzi waga
            */
            this.inputs = inputs; //Aktualizacja danych wejściowych co każdy FeedForward

            //Wyliczanie wartości neuronów
            for (int i = 0; i < numberOfOuputs; i++)
            {
                outputs[i] = 0;
                for (int j = 0; j < numberOfInputs; j++)
                {
                    outputs[i] += inputs[j] * weights[i, j];
                }
                //Aktywowanie neuronów funkcją aktywacji
                outputs[i] = Program.ActivactionFunction.Calculate(outputs[i]);
            }

            return outputs;
        }

        /// <summary>
        /// Back propagation dla warstwy wyjściowej
        /// </summary>
        /// <param name="expected">Oczekiwana wartość w warstwie wyjściowej</param>
        public void BackPropOutput(float[] expected)
        {
            #region TAKI ZBĘDNY DODATEK KTÓRY RÓŻNIE DZIAŁA
            totalError = 0.0f;
            //Oblicza MSE
            for (int i = 0; i < numberOfOuputs; i++)
            {
                totalError += ((expected[i] - outputs[i]) * (expected[i] - outputs[i]));
            }
            totalError = (1.0f / 2.0f) * totalError;

            if (totalError > maxError)
                maxError = totalError;
            if (totalError < minError && totalError > 0.0000001f)
                minError = totalError;
            #endregion

            /* Nasza delta (patrz załączone wzory)
             *  Jest to pochodna funkcji MSE (błąd średniokwadratowy)
            */
            for (int i = 0; i < numberOfOuputs; i++)
                error[i] = outputs[i] - expected[i];

            //Wyliczenie gammy (patrz załączone wzory)
            for (int i = 0; i < numberOfOuputs; i++)
                gamma[i] = error[i] * Program.ActivactionFunction.CalculateDerivative(outputs[i]);

            /* Obliczanie wag delta (patrz załączone wzory)
             * Przykład:
             * Wnowa = Wstara - LearningRate * Delta;
            */
            for (int i = 0; i < numberOfOuputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }
        }

        /// <summary>
        /// Back propagation dla warstw ukrytych
        /// </summary>
        /// <param name="gammaForward">the gamma value of the forward layer</param>
        /// <param name="weightsFoward">the weights of the forward layer</param>
        public void BackPropHidden(float[] gammaForward, float[,] weightsFoward)
        {
            //Obliczanie gammy dla warstw ukrytych (patrz wzory)
            for (int i = 0; i < numberOfOuputs; i++)
            {
                gamma[i] = 0;

                for (int j = 0; j < gammaForward.Length; j++)
                {
                    gamma[i] += gammaForward[j] * weightsFoward[j, i];
                }

                gamma[i] = gamma[i] * Program.ActivactionFunction.CalculateDerivative(outputs[i]);
            }

            //Obliczanie weightsDelta
            for (int i = 0; i < numberOfOuputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weightsDelta[i, j] = gamma[i] * inputs[j];
                }
            }
        }

        /// <summary>
        /// Aktualizacja wag
        /// </summary>
        public void UpdateWeights()
        {
            for (int i = 0; i < numberOfOuputs; i++)
            {
                for (int j = 0; j < numberOfInputs; j++)
                {
                    weights[i, j] = weights[i, j] - Program.LearningRate * weightsDelta[i, j];
                }
            }
        }
    }
}
