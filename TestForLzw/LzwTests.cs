using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestForLzw
{
    [TestClass]
    public class LzwTests
    {   
        private static bool FilesAreEqual(string path1, string path2)
        {
            using FileStream file1 = File.OpenRead(path1);
            using FileStream file2 = File.OpenRead(path2);
            if (file1.Length != file2.Length)
            {
                file1.Close();
                file2.Close();
                return false;
            }
            for (int i = 0; i < file1.Length; i++)
            {
                if (file1.ReadByte() != file2.ReadByte())
                {
                    file1.Close();
                    file2.Close();
                    return false;
                }
            }
            file1.Close();
            file2.Close();
            return true;
        }

        [TestMethod]
        public void TestCorrectCompressDecompressTxt()
        {
            // setup
            string path1 = "..\\..\\..\\testFile.txt";
            string path2 = "..\\..\\..\\test.txt";
            File.Copy(path1, path2);
            LzwAlgorithm.LZW.Lzw(path2);
            File.Delete(path2);
            LzwAlgorithm.LZW.ReverseLzw(path2 + ".zipped");
            // run
            bool areEqual = FilesAreEqual(path1, path2);
            // assert
            File.Delete(path2);
            File.Delete(path2 + ".zipped");
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void TestCorrectCompressDecompressExe()
        {
            // setup
            string path1 = "..\\..\\..\\gta_sa.exe";
            string path2 = "..\\..\\..\\test.exe";
            File.Copy(path1, path2);
            LzwAlgorithm.LZW.Lzw(path2);
            File.Delete(path2);
            LzwAlgorithm.LZW.ReverseLzw(path2 + ".zipped");
            // run
            bool areEqual = FilesAreEqual(path1, path2);
            // assert
            File.Delete(path2);
            File.Delete(path2 + ".zipped");
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void TestCorrectCompressDecompressImg()
        {
            // setup
            string path1 = "..\\..\\..\\testImg.bmp";
            string path2 = "..\\..\\..\\test.bmp";
            File.Copy(path1, path2);
            LzwAlgorithm.LZW.Lzw(path2);
            File.Delete(path2);
            LzwAlgorithm.LZW.ReverseLzw(path2 + ".zipped");
            // run
            bool areEqual = FilesAreEqual(path1, path2);
            // assert
            File.Delete(path2);
            File.Delete(path2 + ".zipped");
            Assert.IsTrue(areEqual);
        }
    }
}