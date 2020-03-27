using System;

namespace snake
{
    public class Neurone
    {
        /**
         * Current value held by the neurone
         */
        public double value;
        
        /**
         * Weights and biais of the neurone
         */
        private double[] weights;
        private double biais;

        public double[] Weights
        {
            get { return weights; }
            set { weights = value; }
        }

        public double Biais
        {
            get { return biais; }
            set { biais = value; }
        }

        static Random rand = new Random();

        /**
         * Build a new random neurone regarding the size of the previous layer
         */
        public Neurone(int prev_size)
        {
            Random rnd = new Random();
            value = 0;
            weights = new double[prev_size];
            for (int i = 0; i < prev_size; ++i)
                weights[i] = rnd.NextDouble() * 2 - 1;
            biais = rnd.NextDouble() * 2 - 1;
        }

        /**
         * Create a copy of the neurone given in parameter
         * if mutate is true, apply mutations the copy
         */
        public Neurone(Neurone neurone, bool mutate = true)
        {
            value = 0;
            weights = new double[neurone.weights.Length];
            biais = neurone.biais;
            for (int i = 0; i < weights.Length; ++i)
                weights[i] = neurone.weights[i];
            
            if (mutate)
                Mutate();
        }

        /**
         * Mutation function
         * Apply mutations to the weights and biais of the neurone
         */
        public void Mutate()
        {
            biais = biais * (1 + ((rand.NextDouble() - 0.5) * 0.5));
            int len = weights.Length;
            for(int i = 0; i < len; i++)
            {
                weights[i] = weights[i] * (1 + ((rand.NextDouble() - 0.5) * 0.5));
            }
        }

        /**
         * Simple feed forward
         * computes the weighted sum and apply the activation function
         */
        public void FrontProp(Layer prevLayer, bool isLast = false)
        {
            double sum = 0;
            for (int i = 0; i < weights.Length; ++i)
                sum += prevLayer.Neurones[i].value * weights[i];

            sum += biais;
            //Uncomment the following line if you implement the softmax bonus
            //if (!isLast)
                sum = Activation(sum);
            value = sum;
        }

        /**
         * Activation function of regular layers: Smooth-Relu
         */
        public double Activation(double x)
        {
            return Math.Log(1 + Math.Exp(x));
        }

        /**
         * Apply softmax on the neurone
         * factor is the normalization factor computed from the layer
         * FIXME
         */
        public double SoftMax(double factor)
        {
            return factor;
        }

        /**
         * Merge the neurone with the one given in parameter in place
         * FIXME
         */
        public void Mix(Neurone partner)
        {
        }
    }
}
