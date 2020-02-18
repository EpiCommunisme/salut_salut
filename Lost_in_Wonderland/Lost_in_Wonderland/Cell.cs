namespace Lost_in_Wonderland
{
    public class Cell
    {
        private bool up;
        private bool down;
        private bool left;
        private bool right;
        private bool start;
        private bool end;
        private bool is_path;
        
        public Cell(bool start, bool end)
        {
            up = true;
            down = true;
            left = true;
            right = true;
            this.start = start;
            this.end = end;
            is_path = false;
        }

        public bool Up
        {
            get => up;
            set => up = value;
        }

        public bool Down
        {
            get => down;
            set => down = value;
        }

        public bool Left
        {
            get => left;
            set => left = value;
        }

        public bool Right
        {
            get => right;
            set => right = value;
        }

        public bool Start
        {
            get => start;
            set => start = value;
        }

        public bool End
        {
            get => end;
            set => end = value;
        }
        
        public bool IsPath
        {
            get => is_path;
            set => is_path = value;
        }
    }
}