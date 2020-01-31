using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace WonderlandTycoon
{
    internal class Program
    {
        private const int NB_ROUNDS = 100;
        private const long INITIAL_MONEY = 11000;

        public static void Main(string[] args)
        {
            // Put your tests here like the example (don't forget to delete them before submitting)
            TestsAttraction();

            // TODO : Remove the following line when you finished the Basics (up to Game.cs)
            return;

            // These lines are launching the game and then the viewer
            Game game = new Game("agave_plantation.map", NB_ROUNDS, INITIAL_MONEY);
            long score = game.Launch(new MyBot());
            Console.WriteLine("Score: {0}", score);
            TycoonIO.Viewer();
        }

        static private void TestsAttraction()
        {
            Attraction myFirstAttraction = new Attraction();
            long myMoney = INITIAL_MONEY;

            Console.WriteLine("Before upgrade of myFirstAttraction :");
            Console.WriteLine(" -> myFirstAttraction.Type = " + myFirstAttraction.Type + " (Should be: ATTRACTION)");
            Console.WriteLine(" -> myMoney = " + myMoney + " (Should be: 11000)");
            Console.WriteLine(" -> myFirstAttraction.Attractiveness() = " + myFirstAttraction.Attractiveness() 
                                                                          + " (Should be: 500)");
            myFirstAttraction.Upgrade(ref myMoney);

            Console.WriteLine();
            Console.WriteLine("After upgrade of myFirstAttraction :");
            Console.WriteLine(" -> myMoney = " + myMoney + " (Should be: 6000)");
            Console.WriteLine(" -> myFirstAttraction.Attractiveness() = " + myFirstAttraction.Attractiveness() 
                                                                          + " (Should be: 1000)");

            Console.WriteLine();
            Console.WriteLine("Try to upgrade with not enough money :");
            Console.WriteLine(" -> myFirstAttraction.Upgrade(ref myMoney) = " + myFirstAttraction.Upgrade(ref myMoney)
                                                                              + " (Should return: False)");

            long myFriendsMoney = 100000;
            Console.WriteLine();
            Console.WriteLine("Try to upgrade twice with more money :");
            Console.WriteLine(" -> " + myFirstAttraction.Upgrade(ref myFriendsMoney)
                                     + " " + myFirstAttraction.Upgrade(ref myFriendsMoney) 
                                     + " (Should be: True True)");
            Console.WriteLine("After upgrade of myFirstAttraction twice :");
            Console.WriteLine(" -> myFriendsMoney = " + myFriendsMoney + " (Should be: 45000)");
            Console.WriteLine(" -> myFirstAttraction.Attractiveness() = " + myFirstAttraction.Attractiveness() 
                                                                          + " (Should be: 1500)");

            Console.WriteLine();
            myMoney = 1000000;
            Console.WriteLine("Try to upgrade myFirstAttraction (can't more thant lvl3) :");
            Console.WriteLine(" -> myFirstAttraction.Upgrade(ref myMoney) = " + myFirstAttraction.Upgrade(ref myMoney) 
                                                                              + " (Should return: False)");
        }
    }
}
