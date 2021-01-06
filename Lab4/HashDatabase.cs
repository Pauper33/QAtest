using Xunit;
using IIG.PasswordHashingUtils;
using IIG.CoSFE.DatabaseUtils;

namespace HashDatabase
{
    public class HashDatabase
    {
        public class Authorization
        {
            private const string Server = @"DESKTOP-V1GHIVR\MSSQLSERVER2";
            private const string Database = @"IIG.CoSWE.AuthDB";
            private const bool IsTrusted = false;
            private const string Login = @"sa";
            private const string Password = @"25022000";
            private const int ConnectionTimeOut = 75;
            public AuthDatabaseUtils ConnectionDB = new AuthDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTimeOut);
        }

        string[] Users = new string[4] { "Adolf Hitler", "Joseph Stalin", "Winston Churchill", "Franklin Roosevelt" };
        string[] Pass = new string[4] { "Sieg heil", "I serve the Soviet Union", "I serve my Queen", "for the independence of the United States" };
        string[] Salt = new string[4] { "Deutschland", "USSR", "England", "USA" };

        Authorization SQL_connection = new Authorization();

        [Fact]
        public void InitUser()
        {
            Assert.True(SQL_connection.ConnectionDB.AddCredentials(Users[0], PasswordHasher.GetHash(Pass[0], Salt[0])));
            Assert.True(SQL_connection.ConnectionDB.AddCredentials(Users[1], PasswordHasher.GetHash(Pass[1], Salt[1])));
            Assert.True(SQL_connection.ConnectionDB.AddCredentials(Users[2], PasswordHasher.GetHash(Pass[2], Salt[2])));
            Assert.True(SQL_connection.ConnectionDB.AddCredentials(Users[3], PasswordHasher.GetHash(Pass[3], Salt[3])));
        }


        [Fact]
        public void DeleteUsers()
        {
            Assert.True(SQL_connection.ConnectionDB.DeleteCredentials(Users[0], PasswordHasher.GetHash(Pass[0], Salt[0])));
            Assert.True(SQL_connection.ConnectionDB.DeleteCredentials(Users[1], PasswordHasher.GetHash(Pass[1], Salt[1])));
            Assert.True(SQL_connection.ConnectionDB.DeleteCredentials(Users[2], PasswordHasher.GetHash(Pass[2], Salt[2])));
            Assert.True(SQL_connection.ConnectionDB.DeleteCredentials(Users[3], PasswordHasher.GetHash(Pass[3], Salt[3])));
        }
        
        [Fact]
        public void CheckUser()
        {
            Assert.False(SQL_connection.ConnectionDB.CheckCredentials("Piter Pen", PasswordHasher.GetHash("Fly")));
            SQL_connection.ConnectionDB.AddCredentials("Klichko", PasswordHasher.GetHash("Kiev", "Ukraine"));
            Assert.True(SQL_connection.ConnectionDB.CheckCredentials("Klichko", PasswordHasher.GetHash("Kiev", "Ukraine")));
            //SQL_connection.ConnectionDB.DeleteCredentials("Klichko", PasswordHasher.GetHash("Kiev", "Ukraine"));
        }

        [Fact]
        public void EmptyPass()
        {
            Assert.False(SQL_connection.ConnectionDB.AddCredentials("Mr. Presidento", ""));
        }

        [Fact]
        public void EmptyLogin()
        {
            Assert.False(SQL_connection.ConnectionDB.AddCredentials("", PasswordHasher.GetHash("Stalker", "Yachek")));
        }

        [Fact]
        public void LatinPass()
        {
            Assert.False(SQL_connection.ConnectionDB.AddCredentials("Student", "KPI"));
        }

        [Fact]
        public void NonLatinPass()
        {
            Assert.False(SQL_connection.ConnectionDB.AddCredentials("Chotki", "Паца"));
        }

        [Fact]
        public void Byte256Pass()
        {
            Assert.True(SQL_connection.ConnectionDB.AddCredentials("Bad Guy", "{}E2EFCCAF81565FD91742D5C8F877E2ED524A92C5899D299D4372E8565AFA80"));
        }

        [Fact]
        public void UpdateUser()
        {
            Assert.True(SQL_connection.ConnectionDB.UpdateCredentials("Bad Guy", "{}E2EFCCAF81565FD91742D5C8F877E2ED524A92C5899D299D4372E8565AFA80", "Forest", PasswordHasher.GetHash("Run", "Jump")));
        }        

        [Fact]
        public void Byte64Pass()
        {
            Assert.False(SQL_connection.ConnectionDB.AddCredentials("Bill", "ED524A92C5899D299D4372E8565AFA80"));
        }
    }
}
