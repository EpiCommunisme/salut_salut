using System;
using System.Threading;

namespace snake
{
    class Program
    {

        static void Main(string[] args)
        {
            Generation gen = new Generation(200);
            gen.Train(10);
            Bot bot = gen.SelectBot();
            bot.Play(true);
            bot.Save(".save");
        }
    }
}
