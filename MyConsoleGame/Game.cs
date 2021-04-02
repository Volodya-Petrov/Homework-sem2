using System;
using System.IO;

namespace MyConsoleGame
{
    public class Game
    {   
        public Game(string fileName, Action<int, int> CursorForPrintFunction, Action<string> PrintFunction)
        {   
            CursorForPrint = CursorForPrintFunction;
            Print = PrintFunction;
            InitMap(fileName);
        }
        private Action<int, int> CursorForPrint;
        private Action<string> Print;
        private bool[,] map;
        private int x;
        private int y;

        public (int x, int y) GetCoordinates() => (x, y);

        public void OnLeft()
        {
            if (map[x - 1, y] != true)
            {
                Print(" ");
                CursorForPrint(x - 1, y);
                x--;
                Print("@");
                CursorForPrint(x, y);
            }
        }

        public void OnRight()
        {
            if (map[x + 1, y] != true)
            {
                Print(" ");
                CursorForPrint(x + 1, y);
                x++;
                Print("@");
                CursorForPrint(x, y);
            }
        }

        public void OnUp()
        {
            if (map[x, y - 1] != true)
            {
                Print(" ");
                CursorForPrint(x, y - 1);
                y--;
                Print("@");
                CursorForPrint(x, y);
            }
        }

        public void OnDown()
        {
            if (map[x, y + 1] != true)
            {
                Print(" ");
                CursorForPrint(x, y + 1);
                y++;
                Print("@");
                CursorForPrint(x, y);
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
                        CursorForPrint(x, y);
                        Print("#");
                        continue;
                    }
                    if (fileText[y][x] == '@')
                    {
                        this.x = x;
                        this.y = y;
                        CursorForPrint(x, y);
                        Print("@");
                        continue;
                    }
                    CursorForPrint(x, y);
                    Print(" ");
                }
            }
            CursorForPrint(x, y);
        }
    }
}
