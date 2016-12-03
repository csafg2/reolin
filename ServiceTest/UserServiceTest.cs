using System;
using Reolin.Data;
using SqlServerTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data.Services;
using Reolin.Data.Domain;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Validation;

namespace ServiceTest_
{
    [TestClass]
    public class UserServiceTest_
    {
        public UserService Service { get; private set; }

        public UserServiceTest_()
        {
            Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            DataContext context = new DataContext();
            this.Service = new UserService(context);
        }


        [TestMethod]
        public void Test_CrateAsync()
        {
            var user = new User()
            {
                UserName = "Hassan",
                Password = new
                SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes("Hassan@1")),
                Email = "HassanHashemi@yahoo.com"
            };

            this.Service.CreateAsync(user).Wait();
        }

        [TestMethod]
        public void Test_ValidateAsync()
        {

        }

        [TestMethod]
        public void Test_ChangePasswordAsync()
        { }

        [TestMethod]
        public void Test_GetUserByEmailAsync()
        {
        }

        [TestMethod]
        public void Test_GetByUserNameAsync()
        {

        }
    }
}
