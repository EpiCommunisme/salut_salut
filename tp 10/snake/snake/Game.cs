using System;
using System.Runtime.CompilerServices;

namespace snake
{
    public class Game
    {
        public enum Cell
        {
            Empty = 0,
            Snake = 1,
            Apple = -1
        }

        private Cell[,] grid;
        private int score;
        private int turn;
        private int ttl;

        private Snake snake;

        public Snake Snake => snake;

        private int appleX;
        private int appleY;
        private Bot bot;

        private int x;
        private int y;

        private bool lost;

        public Game(Bot bot, int x, int y)
        {
            score = 0;
            this.x = x;
            this.y = y;
            lost = false;
            grid = new Cell[x, y];
            snake = new Snake(x / 2, y / 2);
            NewApple();
            this.bot = bot;
            turn = 0;
            UpdateGrid();
            ttl = 200;
        }

        public void NewApple()
        {
            Random rnd = new Random();
            appleX = rnd.Next() % x;
            appleY = rnd.Next() % y;
            if (grid[appleX, appleY] != Cell.Empty)
                NewApple();
        }


        public void PrintGrid()
        {
            Console.Clear();
            ConsoleColor tmp = Console.ForegroundColor;
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    switch (grid[i, j])
                    {
                        case Cell.Empty:
                            Console.Write(' ');
                            break;
                        case Cell.Apple:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write('O');
                            Console.ForegroundColor = tmp;
                            break;
                        case Cell.Snake:
                            Console.ForegroundColor = ConsoleColor.Green;
                            if (i == snake.Head.X && j == snake.Head.Y)
                                Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write('O');
                            Console.ForegroundColor = tmp;
                            break;
                    }
                    Console.Write(' ');
                }

                Console.WriteLine('|');
            }

            for (int i = 0; i < y * 2; ++i)
                Console.Write('_');
            Console.Write("    Score: ");
            Console.WriteLine(score);
        }

        public bool IsInGrid(int x, int y)
        {
            return x >= 0 && y >= 0 && x < this.x && y < this.y;
        }
        public double[] Scan(int xRay, int yRay)
        {
            double[] ret = new double[3];
            int x = snake.Head.X + xRay;
            int y = snake.Head.Y + yRay;
            int dist = 1;
            while (IsInGrid(x, y))
            {
                if (grid[x, y] == Cell.Apple)
                    ret[0] = 10;
                if (grid[x, y] == Cell.Snake && ret[1] == 0)
                    ret[1] = 10.0 / dist;
                ++dist;
                x += xRay;
                y += yRay;
            }

            ret[2] = 10.0 / dist;
            return ret;
        }
        
        public double[] GridScan()
        {
            double[] info = new double[28];
            int k = 0;
            for (int i = -1; i < 2; ++i)
            {
                for (int j = -1; j < 2; ++j)
                {
                    if (i == 0 && j == 0)
                        continue;
                    double[] rayInfo = Scan(i, j);
                    for (int l = 0; l < 3; ++l)
                    {
                        info[k] = rayInfo[l];
                        ++k;
                    }
                }
            }

            info[k + (int) snake.Dir] = 1;

            return info;
        }

        public void UpdateGrid()
        {
            grid = new Cell[x, y];
            grid[appleX, appleY] = Cell.Apple;
            snake.FillGrid(grid);
        }

        public void PlayTurn(bool display = false)
        {
            ++turn;
            --ttl;
            Snake.Direction dir = bot.PlayTurn(display);
            if ((int)dir == -(int)snake.Dir)
                dir = snake.Dir;
            snake.Dir = dir;
            try
            {
                snake.Move(grid);
            }
            catch (ArgumentException)
            {
                lost = true;
            }

            if (ttl <= 0)
            {
                lost = true;
            }

            UpdateGrid();

            if (appleX == snake.Head.X && appleY == snake.Head.Y)
            {
                ++score;
                snake.Grow();
                if (x * y - 5 - score > 0)
                    NewApple();
                ttl = 200;
            }
            if (display)
                PrintGrid();
        }

        public void SetDirection(Snake.Direction dir)
        {
            snake.Dir = dir;
        }

        public long Play(bool display = false)
        {
            while (!lost)
            {
                PlayTurn(display);
            }

            return (long)((Math.Pow(2, score) + Math.Pow(score, 2.1) * 500) -
                   (Math.Pow(score, 1.2) * Math.Pow(0.25 * turn, 1.3)));
        }
    }
}
