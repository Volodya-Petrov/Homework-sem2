using NUnit.Framework;
using System;
using System.Collections.Generic;
using Task1;

namespace TestForTask
{
    public class Tests
    {
        private Vector vector1;
        private Vector vector2;

        [SetUp]
        public void Setup()
        {
            var dict1 = new Dictionary<int, int>();
            dict1.Add(0, 2);
            dict1.Add(1, 3);
            dict1.Add(2, 5);
            vector1 = new Vector(dict1, 3);
            var dict2 = new Dictionary<int, int>();
            dict2.Add(0, 1);
            dict2.Add(1, 1);
            dict2.Add(2, 1);
            vector2 = new Vector(dict2, 3);
        }

        [Test]
        public void TestForMultiply()
        {
            Assert.AreEqual(10, vector2 * vector1);
        }

        [Test]
        public void TestForNullVector()
        {
            var dict = new Dictionary<int, int>();
            var vector = new Vector(dict, 3);
            Assert.IsTrue(vector.IsNull());
        }

        [Test]
        public void TestForAdding()
        {
            var resultVector = vector1 + vector2;
            Assert.IsTrue(resultVector[0] == 3);
            Assert.IsTrue(resultVector[1] == 4);
            Assert.IsTrue(resultVector[2] == 6);
        }

        [Test]
        public void TestShouldThrowExceptionWhenKeyInDictBiggerThenLengthOfVector()
        {
            var dict = new Dictionary<int, int>();
            dict.Add(5, 5);
            Assert.Throws<CoordinateBiggerLengthOfVectorException>(() => new Vector(dict, 3));
        }
    }
}