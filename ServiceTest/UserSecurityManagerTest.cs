using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Web.Security.Membership;
using Reolin.Web.Security.Membership.Core;
using Reolin.Web.Security.Membership.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceTest
{
    [TestClass]
    public class UserSecurityManagerTest
    {
        IUserSecurityManager _manager;
        DataContext context = new DataContext();
        public UserSecurityManagerTest()
        {
            var service = new UserService(context);
            this._manager = new UserSecurityManager(service, CreateValidators(),
                new SHA1PasswordHasher());
        }

        [TestMethod]
        public void UserSecurityManager_Create()
        {
            int r = this._manager.CreateAsync("Hoola", "123456").Result;

            Assert.IsTrue(r > 0);
        }

        /// <summary>
        /// Finds and instantiates all classes that implement "IUserValidator"
        /// </summary>
        /// <returns>a list of "IUserValidator" objects</returns>
        private static List<IUserValidator> CreateValidators()
        {
            List<IUserValidator> result = new List<IUserValidator>();
            Type interfaceType = typeof(IUserValidator);
            
            typeof(UserEmailValidator)
                .Assembly
                    .GetTypes()
                        .Where(t => !t.IsInterface && interfaceType.IsAssignableFrom(t))
                            .ForEach(t => result.Add((IUserValidator)Activator.CreateInstance(t)));

            return result;
        }
    }

    #region nothing
    public static class Fuck
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }
    }
    #endregion
}
