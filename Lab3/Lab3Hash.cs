using System;
using Xunit;
using IIG.PasswordHashingUtils;



namespace Lab3Hash

{
    public class TestPasswordHashingUtils 
    {
        public class PassHashConstructor
        {
            public class TestPass 
            {
                [Fact]
                public void NullPass()
                {
                    Assert.Throws<ArgumentNullException>(() => PasswordHasher.GetHash(null, null, null));
                }
                [Fact]
                public void EmptyPass()
                {
                    string acc = PasswordHasher.GetHash("", null, null);
                    Assert.NotNull(acc);
                    Assert.Equal(acc, PasswordHasher.GetHash("", null, null));
                }
                [Fact]
                public void JapanesePass() 
                {
                    string acc = PasswordHasher.GetHash("私の窓の下の木", null, null);
                    Assert.NotNull(acc);
                    Assert.Equal(acc, PasswordHasher.GetHash("私の窓の下の木", null, null));
                }
                [Fact]
                public void LongPassWithLongChecksum()
                {
                    string acc = PasswordHasher.GetHash("Its a very long password and i hope that my pass willn`t be forget", null, 1231231123);
                    Assert.NotNull(acc);
                    Assert.NotEqual(acc, PasswordHasher.GetHash("Its a very long password and i hope that my pass willn`t be forget", null, 99999999));
                }
            }

            public class TestSalt 
            {
                [Fact]
                public void EmptySalt()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", "", null);
                    Assert.NotNull(acc);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa", "", null));
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa", null, null));
                }
               
                [Fact]
                public void JapaneseSalt()
                {
                    Assert.Throws<OverflowException>(() => PasswordHasher.GetHash("white cocoa", "私の窓の下の木"));
                }
                
            }
            public class TestChecksum 
            {
                [Fact]
                public void EmptyChecksum()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", null, null);
                    Assert.NotNull(acc);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa", null, null));
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa", null, 0));
                }
                [Fact]
                public void MaxChecksum()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", null, 4294967295);
                    Assert.NotNull(acc);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa", null, 4294967295));
                }
            }
            public class TestInit 
            {
                [Fact]
                public void StaticInit()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", null, null);
                    PasswordHasher.Init(null, 0);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa", null, null));
                }
                [Fact]
                public void NotStaticInit()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", null, null);
                    PasswordHasher.Init("white cocoa", 0);
                    Assert.NotEqual(acc, PasswordHasher.GetHash("white cocoa"));
                }
                [Fact]
                public void InitAnotherSalt()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", "InitAnotherSalt", null);
                    PasswordHasher.Init("InitAnotherSalt", 0);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa"));
                }
                [Fact]
                public void InitAnotherCheckSum()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", null, 32);
                    PasswordHasher.Init(null, 32);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa"));
                }
                [Fact]
                public void InitAnotherArguments()
                {
                    string acc = PasswordHasher.GetHash("white cocoa", "white salt", 42);
                    PasswordHasher.Init("white salt", 42);
                    Assert.Equal(acc, PasswordHasher.GetHash("white cocoa"));
                }
            }
        }
    }
}
