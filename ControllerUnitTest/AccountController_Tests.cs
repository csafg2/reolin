using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Web.Api.Controllers;
using Reolin.Web.ViewModels;
using Reolin.Web.Security.Jwt;
using Reolin.Web.Security.Membership;
using Reolin.Web.Security.Membership.Core;
using Reolin.Web.Security.Membership.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Reolin.Web.Api.Models;

namespace ControllerUnitTest
{
    [TestClass]
    public class AccountController_Tests
    {
        AccountController _controller;
        DataContext _context = new DataContext();

        public AccountController_Tests()
        {
            var service = new UserService(_context);
            var manager = new UserSecurityManager(service, CreateValidators(), new SHA1PasswordHasher());
            this._controller = new AccountController(manager, 
                TokenOptions, 
                new JwtManager(new InMemoryJwtStore(), new JwtProvider()),
                new DataContext(@"Server=Layoot.yottagroup.com\SQLEXPRESS; Database =ReolinDb; User Id=sa; Password=rrfa7714@1;"));
        }
        
        [TestMethod]
        public void AccountController_Register()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 10; i++)
            {
                var result = this._controller.Login(new LoginViewModel()
                {
                    Password = "213123",
                    UserName = "Hassan"
                }).Result;
            }
            watch.Stop();
            var first = watch.Elapsed;
            watch.Reset();
            watch.Start();

            for (int i = 0; i < 10; i++)
            {
                var result = _controller.Login(new LoginViewModel()
                {
                    Password = "213123",
                    UserName = "Hassan"
                }).Result;
            }

            watch.Stop();
        }

        [TestMethod]
        public void Suggestion_Test()
        {
            var controller = new SuggestionController(new SuggestionService(_context), _context);
            var result = controller.Search(new SearchSuggestionModel()
            {
                Query = "android",
                SourceLat = 35.6891975,
                SountLong = 51.3889736
            }).Result;

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
