using Reolin.Web.Security.Membership.Core;
using System;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.validators
{
    public class UserNameValidator : IUserValidator
    {
        private readonly string[] invalidStrings = new string[] { "-", "@" };
        public Task<IdentityResult> ValidateEmail(string email)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidatePassword(string password)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            foreach (var item in invalidStrings)
            {
                if (userName.Contains(item))
                {
                    return Task.FromResult(IdentityResult.Failed(new InvalidUserNameException($"username can not contain '{item}'")));
                }
            }

            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}
