using System;
using System.Threading;


namespace snake
{
    public class Generation
    {
        /**
         * The array containing all the bots of the population
         */
        private Bot[] generation;

        static Random rand = new Random();
        /**
         * Create a new random Generation of the size given in parameter,
         * all playing on a 8 * 8 Grid
         */
        public Generation(int size)
        {
            generation = new Bot[size];
            for (int i = 0; i < size; ++i)
                generation[i] = new Bot(8, 8);
        }

        /**
         * Sort bots by decreasing order of fitness
         * FIXME
         */
        void Sort()
        {
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < generation.Length - 1; i++)
                {
                    if (generation[i].sumScore < generation[i + 1].sumScore)//(generation[i].Score < generation[i + 1].Score)
                    {
                        sorted = false;
                        Bot swap = generation[i];
                        generation[i] = generation[i + 1];
                        generation[i + 1] = swap;
                    }
                }
            }
        }

        /**
         * HINT: Return an array of the n best bots of the generation
         * FIXME
         */
        public Bot[] GetBestBots(int n)
        {
            Bot[] val = new Bot[n];
            for(int i = 0; i < n; i++)
            {
                val[i] = generation[i];
            }
            return val;
        }

        /**
         * HINT: Bot selection for the next generation,
         * the fitness parameter represents the sum of the scores
         * of every bot of the generation
         * FIXME
         */
        public Bot SelectBot(long fitness_sum)
        {
            Bot bot = GetBestBots(1)[0];
            return bot;
        }


        /**
         * HINT: Bot selection for the next generation, no parameter,
         * naive implementation
         * FIXME
         */
        public Bot SelectBot()
        {
            return generation[0];
        }

        /**
         * HINT: Copy, crossover and mutate the previous generation bots into
         * the gen array in parameter
         * FIXME
         */
        public void Duplicate(Bot[] gen)
        {
            int keep = (int)(generation.Length * 0.2);
            Bot[] best = GetBestBots(keep);
            for (int i = 0; i < best.Length; i++)
            {
                gen[i] = best[i];
            }
            for (int i = best.Length; i < gen.Length; i++)
            {
                gen[i] = new Bot(SelectBot(), true);
            }
        }

        /**
         * Create a new generation, if the parameter is true,
         * the best bot of the previous generation must play a new game and be
         * displayed on stdout
         * FIXME
         */
        public void NewGen(bool display_best = false)
        {
            if (display_best)
            {
                generation[0].Play(true);
            }

            Bot[] gen = new Bot[generation.Length];
            Sort();
            Duplicate(gen);
            generation = gen;
        }

        /**
         * Each bot of the population play a game
         */
        public void Play(bool display = false)
        {
            for (int i = 0; i < generation.Length; ++i)
            {
                 generation[i].Play(display);
            }
        }

        /**
         * Train a population for nbGen generations
         * FIXME
         */
        public void Train(int nbGen)
        {
            for (int i = 0; i < nbGen; i++)
            {
                Play();
                NewGen();
            }
        }

        /**
         * Create a generation with 'size' Bots all restores from the file
         * given in parameter
         */
        public Generation(int size, string path)
        {
            generation = new Bot[size];
            Bot saved = new Bot(path);
            for (int i = 0; i < size; i++)
                generation[i] = new Bot(saved, false);
        }
    }
}
