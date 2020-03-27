using System;

namespace snake
{
    public class Layer
    {
        /**
         * The neurones of the layer
         */
        private Neurone[] neurones;


        public Neurone[] Neurones
        {
            get { return neurones; }
            set { neurones = value; }
        }

        /**
         * Create a new random layer of the given size
         * Neurones of the layer have 'prev_size' weights,
         * which is the size of the previous layer
         */
        public Layer(int size, int prev_size)
        {
            neurones = new Neurone[size];
            
            for (int i = 0; i < size; ++i)
                neurones[i] = new Neurone(prev_size);
        }

        /**
         * Create a copy of the layer given in parameter,
         * if mutate is true, apply mutations to the copy
         */
        public Layer(Layer layer, bool mutate = true)
        {
            neurones = new Neurone[layer.Neurones.Length];
            for (int i = 0; i < layer.Neurones.Length; ++i)
                neurones[i] = new Neurone(layer.Neurones[i], mutate);
        }

        /**
         * Simple feed forward
         * The previous layer is given in parameter to perform the weighted sums
         * isLast indicates if this layer is the last
         */
        public void FrontProp(Layer prevLayer, bool isLast = false)
        {
            for (int i = 0; i < neurones.Length; ++i)
                neurones[i].FrontProp(prevLayer, isLast);
            //if (isLast)
            //SoftMax();
        }

        /**
         * Apply softmax to the layer
         * FIXME
         */
        public void SoftMax()
        {
            double sum = 0;
            for (int i = 0; i < neurones.Length; ++i)
                sum += neurones[i].value;

            for (int i = 0; i < neurones.Length; ++i)
                neurones[i].value = neurones[i].SoftMax(neurones[i].value/sum);
        }

        /**
         * Merge the layer with one given in parameter in place
         * FIXME
         */
        public void Mix(Layer partner)
        {
            for (int i = 0; i < neurones.Length; ++i)
                neurones[i].Mix(partner.neurones[i]);
        }

        /**
         * Apply mutations to all the neurons of the layer
         */
        public void Mutate()
        {
            for (int i = 0; i < neurones.Length; ++i)
                neurones[i].Mutate();
        }
    }
}
