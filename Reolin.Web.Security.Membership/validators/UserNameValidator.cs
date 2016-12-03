using Reolin.Web.Security.Membership.Core;
using System;
using System.Threading.Tasks;
using Reolin.Data.Domain;

namespace Reolin.Web.Security.Membership.validators
{
    public class UserNameValidator : IUserValidator
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

        string[] invalidStrings = new string[] { "-", "@" };
        public Task<IdentityResult> ValidateUserName(string userName)
        {
            foreach (var item in invalidStrings)
            {
                if (userName.Contains(item))
                {
                    return Task.FromResult(IdentityResult.Failed(new InvalidOperationException($"username can not contain '{item}'")));
                }
            }

            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}
