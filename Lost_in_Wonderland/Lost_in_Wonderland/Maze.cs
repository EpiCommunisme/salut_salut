using System;

namespace Lost_in_Wonderland
{
    public class Maze
    {
        public enum Direction
        {
            NORTH,
            SOUTH,
            EAST,
            WEST,
            NULL
        }
        private Cell[,] maze;
        private int size;

        public Maze(int size)
        {
            this.size = size;
            maze = new Cell[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1)
                    {
                        maze [i, j] = new Cell(true,false);
                    }
                    else if (i == 0 && j == 0)
                    {
                        maze [i, j] = new Cell(false,true);
                    }
                    else
                    {
                        maze [i, j] = new Cell(false,false);
                    }
                }
            }
        }
        
        #region PrettyPrint
        
        public void CarvePath(int i, int j, Direction dir)
        {
            if (i >= 0 && i < size && j >= 0 && j < size)
            {
                return;
            }
            if (dir == Direction.EAST && i < size - 1)
            {
                maze[i, j].Right = false;
            }
            if (dir == Direction.WEST && i > 0)
            {
                maze[i-1, j].Right = false;
            }
            if (dir == Direction.SOUTH && j < size - 1)
            {
                maze[i, j].Down = false;
            }
            if (dir == Direction.NORTH && j > 0)
            {
                maze[i, j-1].Down = false;
            }
        }
        
        #endregion
        
        #region PrettyPrint

        public string Bord()
        {
            string res = "==";
            for (int i = 0; i < size; i++)
            {
                res = res + "======";
            }

            return res;
        }

        public string Interieur(int j)
        {
            string res = "||";
            for (int i = 0; i < size; i++)
            {
                if (maze[i, j].Start) res = res + "SSSS";
                if (maze[i, j].End) res = res + "EEEE";
                if (maze[i, j].IsPath) res = res + "PPPP";
                if (!maze[i, j].Start && !maze[i, j].End) res = res + "    ";
                if (i < size - 1)
                {
                    if (maze[i, j].IsPath && maze[i + 1, j].IsPath) res = res + "PP";
                    else if (maze[i, j].Right) res = res + "||";
                    else if (!maze[i, j].Right) res = res + "  ";
                }
                else if (maze[i, j].Right) res = res + "||";
                else if (!maze[i, j].Right) res = res + "  ";
            }

            return res;
        }

        public string BordInterieur(int j)
        {
            string res = "==";
            for (int i = 0; i < size; i++)
            {
                if (maze[i, j].Down) res = res + "====";
                else if (i < size - 1)
                {
                    if (maze[i, j].IsPath && maze[i + 1, j].IsPath) res = res + "PPPP";
                    else res = res + "    ";
                }
                else res = res + "    ";
                res = res + "==";
            }

            return res;
        }
        
        public string MazeToString()
        {
            string res = Bord() + "\n";
            for (int i = 0; i < size; i++)
            {
                res = res + Interieur(i) + "\n";
                res = res + Interieur(i) + "\n";
                res = res + BordInterieur(i) + "\n";
            }

            return res;
        }
        
        #endregion
        
        #region Backtracking 

        public bool IsExit(int i, int j)
        {
            //TODO
            throw new NotImplementedException();
        }
        
        public void MarkPath(int i, int j)
        {
            maze[i, j].IsPath = !maze[i, j].IsPath;
        }

        public bool IsPath(int i, int j)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool IsValidCell(int i, int j)
        {
            //TODO
            throw new NotImplementedException();return (i >= 0 && i < size && j >= 0 && j < size);
        }

        public bool IsValidDirection(int i, int j, Direction direction)
        {
            //TODO
            throw new NotImplementedException();
        }
        
        #endregion
    }
}