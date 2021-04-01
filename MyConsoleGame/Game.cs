using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyConsoleGame
{
    class Game
    {   
        public Game(string fileName)
        {
            InitMap(fileName);
        }
        private bool[,] map;
        private int x;
        private int y;

        public void OnLeft()
        {
            if (map[x - 1, y] != true)
            {
                Console.Write(" ");
                Console.SetCursorPosition(x - 1, y);
                x--;
                Console.Write("@");
                Console.SetCursorPosition(x, y);
            }
        }

        public void OnRight()
        {
            if (map[x + 1, y] != true)
            {
                Console.Write(" ");
                Console.SetCursorPosition(x + 1, y);
                x++;
                Console.Write("@");
                Console.SetCursorPosition(x, y);
            }
        }

        public void OnUp()
        {
            if (map[x, y - 1] != true)
            {
                Console.Write(" ");
                Console.SetCursorPosition(x, y - 1);
                y--;
                Console.Write("@");
                Console.SetCursorPosition(x, y);
            }
        }

        public void OnDown()
        {
            if (map[x, y + 1] != true)
            {
                Console.Write(" ");
                Console.SetCursorPosition(x, y + 1);
                y++;
                Console.Write("@");
                Console.SetCursorPosition(x, y);
            }
        }

        private void InitMap(string fileName)
        {
            var fileText = File.ReadAllLines(fileName);
            map = new bool[fileText[0].Length, fileText.Length];
            for (int y = 0; y < fileText.Length; y++)
            {
                for (int x = 0; x < fileText[0].Length; x++)
                {
                    if (fileText[y][x] == '#')
                    {
                        map[x, y] = true;
                        Console.SetCursorPosition(x, y);
                        Console.Write('#');
                        continue;
                    }
                    if (fileText[y][x] == '@')
                    {
                        this.x = x;
                        this.y = y;
                        Console.SetCursorPosition(x, y);
                        Console.Write('@');
                        continue;
                    }
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');
                }
            }
            Console.SetCursorPosition(x, y);
        }
    }
}
