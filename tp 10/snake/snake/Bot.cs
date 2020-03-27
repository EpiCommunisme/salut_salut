using System;
using System.Numerics;

namespace snake
{
    public class Bot
    {

        /**
         * The underlying NeuralNetwork of the Bot
         */
        private NeuralNetwork network;

        public NeuralNetwork Network => network;

        /**
         * The Game the Bot is playing
         */
        public Game game;

        /**
         * The score received by the game
         */
        private long score;
        public long sumScore;

        /**
         * Dimensions of the grid
         */
        public int x;
        public int y;

        public long Score
        {
            get => score;
        }

        /**
         * Create a Bot playing on x * y grid
         */
        public Bot(int x = 8, int y = 8)
        {
            int[] sizes = { 28, 22, 22, 4 };
            this.x = x;
            this.y = y;
            network = new NeuralNetwork(sizes);
            score = 0;
        }

        /**
         * Create a Bot which is a copy of the parameter
         * Apply mutations if mutate is true
         */
        public Bot(Bot bot, bool mutate = true)
        {
            x = bot.x;
            y = bot.y;
            network = new NeuralNetwork(bot.Network, mutate);
            score = bot.Score;
        }


        /**
         * Make the Bot play a game and display it on the terminal if display
         * if true
         */
        public long Play(bool display = false)
        {
            game = new Game(this, x, y);
            score = game.Play(display);
            return score;
        }

        /**
         * Choose a direction regarding the output layer of the network
         * FIXME
         */
        public Snake.Direction PlayTurn(bool display = false)
        {
            if (display)
            {
                System.Threading.Thread.Sleep(30);
            }

            network.Feed(game.GridScan());
            network.FrontProp();

            Neurone[] output = network.Output();

            int imax = 0;
            int len = output.Length;
            for (int i = 1;i < len; i++)
            {
                if (output[i].value > output[imax].value)
                {
                    imax = i;
                }
            }
            Snake.Direction val = Snake.Direction.LEFT;
            if (imax == 0)
                val = Snake.Direction.LEFT;
            else if(imax == 1)
                val = Snake.Direction.DOWN;
            else if (imax == 2)
                val = Snake.Direction.UP;
            else if (imax == 3)
                val = Snake.Direction.RIGHT;

            return val;
        }

        /**
         * Create a new Bot by merging the current object and the parameter
         */
        public Bot Crossover(Bot partner)
        {
            return new Bot(this, false).Mix(partner);
        }

        /**
         * Merge the current Bot with the parameter in place
         */
        public Bot Mix(Bot partner)
        {
            network.Mix(partner.network);
            return this;
        }

        /**
         * Apply mutations to the NeuralNetwork
         */
        public void Mutate()
        {
            network.Mutate();
        }

        /**
         * Save the Bot to the file given in parameter
         * To submit a Bot to the scoreboard, you must call bot.Save("bot.out")
         * And move the bot.out file as specified in the subject
         */
        public void Save(string path)
        {
            network.Save(path);
        }

        /**
         * Restore a Bot from a file given in parameter
         */
        public Bot (string path)
        {
            x = 8;
            y = 8;
            score = 0;
            network = NeuralNetwork.Restore(path);
        }
    }
}
