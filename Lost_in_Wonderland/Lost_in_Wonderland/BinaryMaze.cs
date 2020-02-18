using System;
using System.Collections.Generic;
using System.Linq;

namespace Lost_in_Wonderland
{
    public class BinaryMaze
    {
        private static List<Maze.Direction> GetDir(Maze maze, int i, int j)
        {
            List<Maze.Direction> res = new List<Maze.Direction>();
            if (i != 0)
            {
                res.Add(Maze.Direction.NORTH);
            }
            if (j != 0)
            {
                res.Add(Maze.Direction.WEST);
            }
            return res;
        }    
        
        public static Maze GenBinaryMaze(int size)
        {
            var random = new Random();
            Maze lab= new Maze(size);
            for (int i = size - 1; i >= 0; i--)
            {
                for (int j = size - 1; j >= 0; j--)
                {
                    List<Maze.Direction> l = GetDir(lab, i, j);
                    int c = l.Count;
                    if (c != 0)
                    {
                        int index = random.Next(0, c);
                        lab.CarvePath(i, j, l[index]);
                    }
                }
            }

            return lab;
        }
    }
}