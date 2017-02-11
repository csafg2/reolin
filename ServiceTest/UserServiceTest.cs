﻿using System;
using Reolin.Data;
using SqlServerTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data.Services;
using Reolin.Data.Domain;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using Reolin.Data.Services.Core;

namespace ServiceTest_
{
    [TestClass]
    public class UserServiceTest_
    {
        public IUserService Service { get; private set; }

        DataContext context = new DataContext();
        
        [TestMethod]
        public void User_QueryProfile()

        {
            var id = context.Users.First().Id;
            var profiles = Service.QueryProfilesAsync(id).Result;

            Assert.IsTrue(profiles.Count() > 0);
        }

        public UserServiceTest_()
        {
            Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);

            DataContext context = new DataContext();
            this.Service = new UserService(context);
        }

        [TestMethod]
        public void User_CreateWithUserNameAndPassword()
        {
        }

        [TestMethod]
        public void User_CrateAsync()
        {
            var user = new User()
            {
                UserName = "Hassan",
                Password = new
                SHA1CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes("123456")),
                Email = "HassanHashemi@yahoo.com"
            };


            int count = this.Service.CreateAsync(user).Result;
            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void User_AddCommentAsync()
        {
            int userId = context.Users.First().Id;
            int profileId = context.Profiles.First().Id;

            int result = this.Service.AddCommentAsync(userId, profileId, "HELLOW COMMENT!").Result;
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void User_ValidateAsync()
        {

        }

        [TestMethod]
        public void User_ChangePasswordAsync()
        { }

        [TestMethod]
        public void User_GetUserByEmailAsync()
        {
        }

        [TestMethod]
        public void User_GetByUserNameAsync()
        {

        }
    }
}
