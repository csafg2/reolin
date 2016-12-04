using Reolin.Web.Security.Membership.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reolin.Data.Domain;

namespace Reolin.Web.Security.Membership.validators
{
    public class UserAlreadyExistValidator : IUserValidator
    {
        public Task<IdentityResult> Validate(User user)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateChangePassword(IUserSecurityManager manager, User user, string oldPassword, string currentPassord)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateEmail(string email)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateStringPassword(string password)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateUserName(string userName)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}
