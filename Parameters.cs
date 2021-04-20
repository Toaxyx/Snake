using System;
using System.Collections.Generic;
using System.Text;

namespace ToaxSnake
{
    public class Parameters
    {
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static int Score { get; set; }
        public static int Speed { get; set; }
        public static int Point { get; set; }
        public static bool GameOver { get; set; }
        public static Direction Direction { get; set; }

        public Parameters()
        {
            Width = 16;
            Height = 16;
            Speed = 16;
            Score = 0;
#if DEBUG
            Point = 100;
#else
            Point = 50;
#endif
            GameOver = false;
            Direction = Direction.Down;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}
