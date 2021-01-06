using System;
using Xunit;
using IIG.BinaryFlag;
using IIG.FileWorker;

namespace BinaryFlagDataFile
{
    public class BinaryFlagDataFile
    {
        private const string path = "$home/GitHub/Lab4/path/";

        [Fact]
        public void WriteToPath()
        {
            MultipleBinaryFlag acc1 = new MultipleBinaryFlag(7, false);
            MultipleBinaryFlag acc2 = new MultipleBinaryFlag(6, true);
            acc1.SetFlag(4);
            acc2.ResetFlag(2);
            Assert.True(BaseFileWorker.Write(acc1.ToString() + " " + acc1.GetFlag() + "\r\n" + acc2.ToString() + " " + acc2.GetFlag() + "\r\n", path + "write.txt"));
        }

        [Fact]
        public void WriteOneFlag()
        {
            MultipleBinaryFlag acc = new MultipleBinaryFlag(7, false);
            acc.SetFlag(4);
            acc.ResetFlag(2);
            Assert.True(BaseFileWorker.Write(acc.ToString() + "\r\n" + acc.GetFlag(), path + "reed.txt"));
        }

        [Fact]
        public void ReedLinesFromFile()
        {
            MultipleBinaryFlag acc = new MultipleBinaryFlag(7, false);
            acc.SetFlag(4);
            string[] lines = BaseFileWorker.ReadLines(path + "reed.txt");
            Assert.Equal(acc.ToString(), lines[0]);
            Assert.Equal(acc.GetFlag().ToString(), lines[1]);
        }

        [Fact]
        public void ReedAllFromFile()
        {
            MultipleBinaryFlag acc1 = new MultipleBinaryFlag(7, false);
            MultipleBinaryFlag acc2 = new MultipleBinaryFlag(6, true);
            acc1.SetFlag(4);
            acc2.ResetFlag(2);
            string[] lines = BaseFileWorker.ReadAll(path + "write.txt").Split("\r\n");
            Assert.Equal(lines[0], acc1.ToString() + " " + acc1.GetFlag());
            Assert.Equal(lines[1], acc2.ToString() + " " + acc2.GetFlag());
        }

    }
}
