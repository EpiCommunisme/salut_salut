namespace snake
{
    public class Node
    {
        private int x;
        private int y;
        private Node next;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
            next = null;
        }

        public void PushBack(Node node)
        {
            if (next == null)
                next = node;
            else
                next.PushBack(node);
        }

        public Node Next
        {
            get => next;
            set => next = value;
        }

        public int X
        {
            get => x;
            set => x = value;
        }

        public int Y
        {
            get => y;
            set => y = value;
        }

        public Node PopBack()
        {
            if (next.Next == null)
            {
                Node ret = next;
                next = null;
                return ret;
            }

            return next.PopBack();
        }

        public void FillGrid(Game.Cell[,] grid)
        {
            grid[x, y] = Game.Cell.Snake;
            if (next == null)
                return;
            next.FillGrid(grid);
        }
    }
}