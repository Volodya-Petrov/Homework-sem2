using NUnit.Framework;
using Routers;
using System.IO;

namespace TestForRouters.test
{
    public class Tests
    {   
        private bool CompareFiles(string firstPath, string secondPath)
        {
            var stringsFirstFile = File.ReadAllLines(firstPath);
            var stringsSecondFile = File.ReadAllLines(secondPath);
            if (stringsFirstFile.Length != stringsSecondFile.Length)
            {
                return false;
            }
            for (int i = 0; i < stringsFirstFile.Length; i++)
            {
                if (stringsFirstFile[i] != stringsSecondFile[i])
                {
                    return false;
                }
            }
            return true;
        }

        [Test]
        public void TestWithCorrectData()
        {
            PrimAlgorithm.WriteMaximunSpanningTree("../../../test1.txt", "../../../resultText1.txt");
            Assert.IsTrue(CompareFiles("../../../ExpectText.txt", "../../../resultText1.txt"));
        }

        [Test]
        public void TestShouldThrowExceptionWhenGraphIsNotConnected()
        {
            var path1 = "../../../test2.txt";
            var path2 = "../../../result2.txt";
            Assert.Throws<GraphIsNotConnectedException>(() => PrimAlgorithm.WriteMaximunSpanningTree(path1, path2));
        }
    }
}