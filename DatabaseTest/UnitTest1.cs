using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Domain;
using System.Security.Cryptography;
using System.Text;

namespace DatabaseTest
{
    [TestClass]
    public class UnitTest1
    {
        DataContext context = new DataContext();

        public UnitTest1()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
        }
        

        [TestMethod]
        public void InsertSimpleUser()
        {
            context.Users.Add(new User()
            {
                UserName = "Hassan",
                FirstName = "Hassan",
                LastName = "Hashemi",
                Email = "HassanHashemi@yahoo.com",
                Password = HashPassword("@123456")
            });
            int count = context.SaveChanges();
            Assert.IsTrue(count > 0);
        }

        private byte[] HashPassword(string v)
        {
            return new SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(v));
        }
    }
}
