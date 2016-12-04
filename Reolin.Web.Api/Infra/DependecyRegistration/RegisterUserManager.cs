using Microsoft.Extensions.DependencyInjection;
using Reolin.Data;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using Reolin.Web.Security.Membership;
using Reolin.Web.Security.Membership.Core;
using Reolin.Web.Security.Membership.Validators;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Reolin.Web
{
    public static class RegisterUserManager
    {
        private static List<IUserValidator> _cache = null;

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


        private static List<IUserValidator> Validators
        {
            get
            {
                return _cache ?? (_cache = CreateValidators());
            }
        }

        public static IServiceCollection AddUserManager(this IServiceCollection source)
        {
            return source
                .AddTransient(typeof(IEnumerable<IUserValidator>), p => Validators)
                .AddTransient<IUserPasswordHasher, SHA1PasswordHasher>()
                .AddTransient(typeof(IUserService), p => new UserService(new DataContext()))
                .AddTransient(typeof(IUserSecurityManager), typeof(UserSecurityManager));
        }
    }
}
