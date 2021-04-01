using System;

namespace MyConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("Map.txt");
            var eventLoop = new EventLoop();
            eventLoop.LeftHandler += game.OnLeft;
            eventLoop.RightHandler += game.OnRight;
            eventLoop.UpHandler += game.OnUp;
            eventLoop.DownHandler += game.OnDown;
            eventLoop.Run();
        }
    }
}
