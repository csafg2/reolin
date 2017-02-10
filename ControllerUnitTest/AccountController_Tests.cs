using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Web.Api.Controllers;
using Reolin.Web.Api.ViewModels;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership;
using Reolin.Web.Security.Membership.Core;
using Reolin.Web.Security.Membership.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ControllerUnitTest
{
    [TestClass]
    public class AccountController_Tests
    {
        AccountController _controller;
        DataContext _context = new DataContext();

        public void Account_Register()
        {
            var service = new UserService(_context);
            var manager = new UserSecurityManager(service, CreateValidators(), new SHA1PasswordHasher());
            this._controller = new AccountController(manager, TokenOptions, new JwtManager(new InMemoryJwtStore(), new JwtProvider()));
        }

        [TestMethod]
        public void AccountController_Register()
        {
            var result = this._controller.Register(new UserRegisterViewModel()
            {
                ConfirmPassword = "Hassan@1",
                Password = "Hassan@1",
                UserName = "Mohammadli"
            }).Result;

            Assert.IsTrue(result is OkResult);

        }

        #region Helpers()
        private static IOptions<TokenProviderOptions> TokenOptions
        {
            get
            {
                return Options.Create(new TokenProviderOptions()
                {
                    Audience = JwtConfigs.Audience,
                    Issuer = JwtConfigs.Issuer,
                    SigningCredentials = JwtConfigs.SigningCredentials,
                    Expiration = JwtConfigs.Expiry
                });
            }
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


        #endregion
    }

    #region FOREACH
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
