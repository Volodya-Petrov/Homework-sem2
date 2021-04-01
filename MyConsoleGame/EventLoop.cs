using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsoleGame
{
    class EventLoop
    {
        public event Action LeftHandler;
        public event Action RightHandler;
        public event Action UpHandler;
        public event Action DownHandler;

        public void Run()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        LeftHandler();
                        break;
                    case ConsoleKey.RightArrow:
                        RightHandler();
                        break;
                    case ConsoleKey.UpArrow:
                        UpHandler();
                        break;
                    case ConsoleKey.DownArrow:
                        DownHandler();
                        break;
                    case ConsoleKey.Escape:
                        return;
                    default:
                        break;
                }
            }
        }
    }
}
