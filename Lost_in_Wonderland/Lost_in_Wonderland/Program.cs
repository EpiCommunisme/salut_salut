using System;

namespace Lost_in_Wonderland
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze game = new Maze(5);
            game.CarvePath(1,0,Maze.Direction.NORTH);
            game.CarvePath(1,0,Maze.Direction.WEST);
            Console.WriteLine(game.MazeToString());
        }
    }
}