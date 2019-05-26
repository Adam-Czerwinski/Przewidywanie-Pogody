namespace NeuralNetwork
{
    public class Network
    {
        /// <summary>
        /// Warstwy w sieci. Nie przechowuje warstwy wejściowej.
        /// </summary>
        private Layer[] layers;

        /// <summary>
        /// Konstrukcja sieci neuronowej
        /// </summary>
        /// <param name="construction">
        /// Konstrukcja sieci.
        /// Zakładając, że tablica wygląda następująco: 
        /// int[] construction = new int[]{20,14,15,10,3}
        /// Oznacza, że warstwa wejściowa ma 20 neuronów
        /// Potem są 3 warstwy ukryte z następującą ilością neuronów: 14,15,10
        /// I na końcu warstwa wyjściowa z 3 neuronami
        /// </param>
        public Network(int[] construction)
        {
            //Głęboka kopia konstrukcji
            //this.construction = new int[construction.Length];
            //for (int i = 0; i < construction.Length; i++)
            //    this.construction[i] = construction[i];


            /*
             * Tworzymy warstwy.
             * Będziemy przetrzymywać o jedną mniej warstwe, ponieważ nie potrzebujemy warstwy wejściowej.
             * W pierwszej warstwie (czyli pierwsza warstwa ukryta) będzie miała wartości neuronów warstwy wejściowej, to wystarczy.
            */
            layers = new Layer[construction.Length - 1];

            /*
             * Teraz tworzymy wszystkie warstwy.
             * Pierwszy parametr kontruktora Layer to liczba neuronów w poprzedniej warstwie.
             * Drugi parametr konstruktora Layer to liczba neuronów w tej warstwie, którą tworzymy
             * A więc construction[0], to warstwa wejściowa 
             * construction[1], to pierwsza warstwa ukryta.
             * Jak widać pierwsza warstwa, która jest warstwą ukrytą przechowuje wartości neuronów. 
            */
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i] = new Layer(construction[i], construction[i + 1]);
            }
        }

        /// <summary>
        /// Total error wykorzystując ostatnią warstwę (warstwa wyjściowa)
        /// </summary>
        /// <returns>MSE</returns>
        public string GetTotalError()
        {
            return layers[layers.Length - 1].totalError.ToString();
        }

        public string GetMinTotalError()
        {
            return layers[layers.Length - 1].minError.ToString();
        }

        public string GetMaxTotalError()
        {
            return layers[layers.Length - 1].maxError.ToString();
        }

        /// <summary>
        /// Feed Forward CAŁEJ sieci.
        /// </summary>
        /// <param name="inputs">
        /// Wartości neuronów w warstwie wejściowej.
        /// </param>
        /// <returns>
        /// Zwraca wartości neuronów w warstwie wyjściowej.
        /// </returns>
        public float[] FeedForward(float[] inputs)
        {
            /*
             * Pierwszej warstwy (która jest pierwsza warstwą ukrytą) nie umieściliśmy w pętli, ponieważ 
             * nie ma poprzedniej warstwy, do których wartości neuronów mogłaby się odwolać, tak jak w przypadku warstwy
             * w pętli odwołują się do wartości neuronów w poprzedniej warstwie (layers[i - 1].outputs).
             * Dla tego przypadku wartościami wejściowymi muszą być wartości neuronów warstwy wejściowej. Danych neuronów nie ustawialiśmy 
             * w konstruktorze, dlatego musimy je podać z zewnątrz (np. w Program.cs). 
             * Podane dane są to wartości neuronów w warstwie wejściowej.
            */
            layers[0].FeedForward(inputs);
            for (int i = 1; i < layers.Length; i++)
                layers[i].FeedForward(layers[i - 1].outputs);

            return layers[layers.Length - 1].outputs; //Zwraca wyniki neuronów z ostatniej warstwy (warstwy wyjściowej)
        }

        /// <summary>
        /// Back Propagation CAŁEJ sieci.
        /// Przynajmniej raz sieć musi przejść Feed Forward
        /// </summary>
        /// <param name="expected">Oczekiwana wartość neuronów na warstwie wyjściowej</param>
        public void BackProp(float[] expected)
        {
            //Wszystkie warstwy zaczynając od tyłu
            for (int i = layers.Length - 1; i >= 0; i--)
            {
                //Ostatnia warstwa ma inną wsteczną propagację
                if (i == layers.Length - 1)
                {
                    layers[i].BackPropOutput(expected);
                }
                else
                {
                    layers[i].BackPropHidden(layers[i + 1].gamma, layers[i + 1].weights);
                }
            }

            //Aktualizacja wag
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].UpdateWeights();
            }
        }

    }
}
