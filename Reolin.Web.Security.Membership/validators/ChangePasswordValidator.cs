using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using System;

namespace Reolin.Web.Security.Membership.Validators
{
    public class PasswordLengthValidator: IUserValidator
    {
        public Task<IdentityResult> ValidateEmail(string email)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateUserName(string userName)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidatePassword(string password)
        {
            if (password.Length > 50)
            {
                return Task.FromResult(IdentityResult.Failed(new InvalidPasswordException("password length can not be over 50")));
            }
            if (password.Length < 6)
            {
                return Task.FromResult(IdentityResult.Failed(new InvalidPasswordException("password length can not be less than 6")));
            }

            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}
