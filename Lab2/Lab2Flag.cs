using System;
using IIG.BinaryFlag;
using Xunit;

namespace Lab2Flag
{
    public class BinaryFlagTest 
    {
        public class BinaryConstructor
        {
            [Fact]
            public void TestWithOneVariable()
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(15);
                Assert.True(acc.GetFlag());
            }
            [Fact]
            public void TestWithTrue()
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(15, true);
                Assert.True(acc.GetFlag());
            }
            [Fact]
            public void TestWithFalse()
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(15, false);
                Assert.False(acc.GetFlag());
            }
            [Fact]
            public void TestOfTwoBinaryEqualFlag()
            {
                MultipleBinaryFlag acc1 = new MultipleBinaryFlag(15, true);
                MultipleBinaryFlag acc2 = new MultipleBinaryFlag(15, true);
                Assert.Equal(acc1.ToString(), acc2.ToString());
            }
            [Fact]
            public void TestOneOfTwoBinaryNotEuqalFlag()
            {
                MultipleBinaryFlag acc1 = new MultipleBinaryFlag(15, false);
                MultipleBinaryFlag acc2 = new MultipleBinaryFlag(15, true);
                Assert.NotEqual(acc1.ToString(), acc2.ToString());
            }
            [Fact]
            public void TestTwoOfTwoBinaryNotEuqalFlag()
            {
                MultipleBinaryFlag acc1 = new MultipleBinaryFlag(14, true);
                MultipleBinaryFlag acc2 = new MultipleBinaryFlag(15, true);
                Assert.NotEqual(acc1.ToString(), acc2.ToString());
            }
            [Fact]
            public void TestBinaryFlagSection() 
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(3, true);
                acc.ResetFlag(2);
                Assert.Equal("TTF", acc.ToString());
            }
            [Fact]
            public void TestBinarySetFlag()
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(2, false);
                acc.SetFlag(1);
                Assert.Equal("FT", acc.ToString());
            }
        }
        public class BinaryType 
        {
            [Fact]
            public void TestBinaryFlagType() 
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(7);
                Assert.Equal("IIG.BinaryFlag.MultipleBinaryFlag", acc.GetType().ToString());
            }
            [Fact]
            public void TestBinaryFlagStringType() 
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(2);
                string str = "TT";
                Assert.Equal(str.GetType(), acc.ToString().GetType());
            }
        }
        public class BunaryLength 
        {
            [Fact]
            public void TestBinaryFlagLength() 
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(5);
                Assert.Equal(5, acc.ToString().Length);
            }
            [Fact]
            public void TestBinaryFlagRangeException()
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(2);

                Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(18446744073709551615));
                Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(17179868705));
                Assert.Throws<System.OutOfMemoryException>(() => new MultipleBinaryFlag(17179868704));
                Assert.True(acc.GetFlag());
                Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(1));
                Assert.Throws<ArgumentOutOfRangeException>(() => new MultipleBinaryFlag(0));

            }
        }
        public class BinaryDispose 
        {
            [Fact]
            public void TestBinaryDispose() 
            {
                MultipleBinaryFlag acc = new MultipleBinaryFlag(2, false);
                acc.Dispose();
                Assert.Equal("FF", acc.ToString());
            }
        }
    }
}
