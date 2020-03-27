using System.Reflection;
using System.IO;
using System;

namespace snake
{
    public class NeuralNetwork
    {
        /**
         * The array of all the layers of the network
         */
        private Layer[] layers;

        /**
         * Create a new random neural network whose dimensions are given
         * in parameter
         */
        public NeuralNetwork(int[] sizes)
        {
            layers = new Layer[sizes.Length];
            for (int i = 0; i < sizes.Length; ++i)
            {
                layers[i] = new Layer(sizes[i], i != 0 ? sizes[i - 1] : 0);
            }
        }

        /**
         * Create a copy of the network given in parameter,
         * if mutate is true, apply mutations to the copy
         */
        public NeuralNetwork(NeuralNetwork network, bool mutate = true)
        {
            layers = new Layer[network.layers.Length];
            
            for (int i = 0; i < network.layers.Length; ++i)
            {
                layers[i] = new Layer(network.layers[i], mutate);
            }
        }

        /**
         * Fill the input layer of the network with the array given in
         * parameter
         */
        public void Feed(double[] input)
        {
            for (int i = 0; i < layers[0].Neurones.Length; ++i)
                layers[0].Neurones[i].value = input[i];
        }

        /**
         * Basic feed forward
         * propagates the input layer to the output layer
         */
        public void FrontProp()
        {
            for (int i = 1; i < layers.Length; ++i)
                layers[i].FrontProp(layers[i - 1], i == layers.Length - 1);
        }

        /**
         * Returns all the neurones of the output layer
         */
        public Neurone[] Output()
        {
            return layers[layers.Length - 1].Neurones;
        }

        /**
         * Merge the neural network with the one given in parameter in place
         * FIXME
         */
        public void Mix(NeuralNetwork partner)
        {
            for (int i = 0; i < layers.Length; ++i)
                layers[i].Mix(partner.layers[i]);
        }

        /**
         * Apply mutations to every layers of the network
         */
        public void Mutate()
        {
            for (int i = 0; i < layers.Length; ++i)
                layers[i].Mutate();
        }

        /**
         * Save the neural network to the file given in parameter
         */
        public void Save(string path)
        {
            string format = "";
            foreach (Layer layer in layers)
            {
                format = layer.Neurones.Length + " " + format;
                format += '\n';
                foreach (Neurone neurone in layer.Neurones)
                {
                    format = format + '\n' + neurone.Biais + " ";
                    foreach (double weight in neurone.Weights)
                    {
                        format = format + weight + " ";
                    }
                }
            }
            format += "\n\n\n";
            File.WriteAllText(path, format);
        }

        /**
         * Retrieve dimensions of the network
         */
        private static int[] GetSizes(string format, ref int i)
        {
            int nb_layers = 0;
            for (int j = 0; format[j] != 0 && format[j] != '\n'; j++)
                if (format[j] == ' ')
                    nb_layers++;

            int[] sizes = new int[nb_layers];
            nb_layers--;

            while (format[i] != 0 && format[i] != '\n') //Sizes
            {
                int end = i;
                while (format[end] != ' ') //Layer's Size
                    end++;

                sizes[nb_layers] = int.Parse(format.Substring(i, end - i));

                nb_layers--;
                i = end + 1;
            }
            i++;
            return sizes;
        }

        /**
         * Restore the neural network
         */
        public static NeuralNetwork Restore(string path)
        {
            string format = File.ReadAllText(path);
            int i = 0;
            int[] sizes = GetSizes(format, ref i);
            NeuralNetwork network = new NeuralNetwork(sizes);
            i++;

            int layer = 0;
            while (format[i] != 0 && format[i] != '\n') //Layers
            {
                int neurone = 0;
                while (format[i] != 0 && format[i] != '\n') //Neurones
                {
                    int j = i;
                    while (format[j] != ' ') //Biais
                        j++;
                    network.layers[layer].Neurones[neurone].Biais = double.Parse(format.Substring(i, j - i));
                    i = j + 1;

                    int weight = 0;
                    while (format[i] != 0 && format[i] != '\n') //Weights
                    {
                        j = i;
                        while (format[j] != ' ')
                            j++;
                        network.layers[layer].Neurones[neurone].Weights[weight] = double.Parse(format.Substring(i, j - i));
                        i = j + 1;
                        weight++;
                    }
                    neurone++;
                    i++;
                }
                layer++;
                i++;
            }
            return network;
        }
    }
}
