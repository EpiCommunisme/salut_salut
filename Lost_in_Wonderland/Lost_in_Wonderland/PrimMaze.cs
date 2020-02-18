using System;
using System.Collections.Generic;

namespace Lost_in_Wonderland
{
    public class PrimMaze
    {
        // Creates a new grid of bool, the same size as the maze
        private static bool[,] InitMarked(int size)
        {
            bool[,] marked = new bool[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    marked[i, j] = false;
            }

            return marked;
        }
        
        // Checks if the cell at index [i,j] is already in the frontier
        private static bool Exists(List<(int, int)> frontier, int i, int j)
        {
            for (int index = 0; index < frontier.Count; index++)
            {
                if (frontier[index].Item1 == i && frontier[index].Item2 == j)
                    return true;
            }
            return false;
        }
        
        private static void Mark(bool[,] marked, List<(int, int)> frontier, int i, int j)
        {
            //TODO
            // Marks the cell [i, j] as visited and add its non visited neighbour to the frontier
        }

        private static Maze.Direction FindNeighbours(bool[,] marked, int i, int j)
        {
            //TODO
            // Finds the directions in which marked neighbours exists
            // Chooses one randomly and returns it
            // If none exists, return the NULL direction
            throw new NotImplementedException();
        }
        
        public static Maze GenPrimMaze(int size)
        {
            //TODO
            // Generates a maze using Prim's algorithm:
            //    1. Choose a random cell
            //    2. Mark it and add its neighbours to the frontier
            //    3. Choose a random cell in the frontier and remove it from the frontier
            //    4. Carve path from this cell to an already marked cell
            //    5. Repeat from step 3 until nothing is left in the frontier
            throw new NotImplementedException();
        }
    }
}