using System;

namespace snake
{
    public class Snake
    {
        public enum Direction
        {
            UP = 1,
            DOWN = -1,
            LEFT = 2,
            RIGHT = -2
        }

        private Direction direction;

        public Direction Dir
        {
            get => direction;
            set => direction = value;
        }

        private Node head;

        public Node Head
        {
            get => head;
        }

        public Snake(int x, int y)
        {
            head = new Node(x, y);
            PushBack(x, y);
            PushBack(x, y);
            PushBack(x, y);
            PushBack(x, y);
            direction = Direction.RIGHT;
        }

        public void PushBack(int x, int y)
        {
            Node newNode = new Node(x, y);
            head.PushBack(newNode);
        }

        public Node PopBack()
        {
            if (head == null)
                return null;
            if (head.Next == null)
            {
                Node ret = head;
                head = null;
                return ret;
            }

            return head.PopBack();
        }

        public void Move(Game.Cell[,] grid)
        {
            Node toMove = PopBack();
            toMove.Next = head;
            int newX = head.X;
            int newY = head.Y;
            switch (direction)
            {
                case Direction.UP:
                    --newX;
                    break;
                case Direction.DOWN:
                    ++newX;
                    break;
                case Direction.LEFT:
                    --newY;
                    break;
                case Direction.RIGHT:
                    ++newY;
                    break;
            }
            if (newX < 0 || newY < 0 || newX >= grid.GetLength(0) || newY >= grid.GetLength(1)
                || grid[newX, newY] == Game.Cell.Snake)
                throw new ArgumentOutOfRangeException();
            head = toMove;
            head.X = newX;
            head.Y = newY;
        }

        public void FillGrid(Game.Cell[,] grid)
        {
            head.FillGrid(grid);
        }

        public void Grow()
        {
            PushBack(head.X, head.Y);
        }
    }
}
