using NUnit.Framework;
using MyConsoleGame;
using System;

namespace TestForConsoleGame
{
    public class Tests
    {
        Game game;

        [SetUp]
        public void Setup()
        {
            Action<int, int> function1 = delegate { };
            Action<string> function2 = delegate { };
            game = new Game("TestMap.txt", function1, function2); 
        }

        [Test]
        public void TestWhenGoLeftCoordinateChange()
        {
            var baseCoordinate = game.GetCoordinates();
            game.OnLeft();
            var resultCoordinate = game.GetCoordinates();
            Assert.AreEqual(baseCoordinate.x - 1, resultCoordinate.x);
            Assert.AreEqual(baseCoordinate.y, resultCoordinate.y);
        }

        [Test]
        public void TestWhenGoDownCoordinateChange()
        {
            var baseCoordinate = game.GetCoordinates();
            game.OnDown();
            var resultCoordinate = game.GetCoordinates();
            Assert.AreEqual(baseCoordinate.x, resultCoordinate.x);
            Assert.AreEqual(baseCoordinate.y + 1, resultCoordinate.y);
        }

        [Test]
        public void TestUpDownAndLeftRightDontChangeCoordinates()
        {
            var baseCoordinate = game.GetCoordinates();
            game.OnDown();
            game.OnUp();
            Assert.AreEqual(baseCoordinate, game.GetCoordinates());
            game.OnLeft();
            game.OnRight();
            Assert.AreEqual(baseCoordinate, game.GetCoordinates());
        }

        [Test]
        public void TestWhenYouGoToWallCoordinatesDontChange()
        {
            var baseCoordinate = game.GetCoordinates();
            game.OnUp();
            Assert.AreEqual(baseCoordinate, game.GetCoordinates());
            game.OnRight();
            Assert.AreEqual(baseCoordinate, game.GetCoordinates());
            game.OnLeft();
            game.OnDown();
            game.OnLeft();
            baseCoordinate = game.GetCoordinates();
            game.OnLeft();
            Assert.AreEqual(baseCoordinate, game.GetCoordinates());
            game.OnDown();
            Assert.AreEqual(baseCoordinate, game.GetCoordinates());
        }
    }
}