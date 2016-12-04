using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using System.Text.RegularExpressions;
using System;

namespace Reolin.Web.Security.Membership.Validators
{
    public class UserEmailValidator: IUserValidator
    {
        public Task<IdentityResult> ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            return regex.Match(email).Success ? 
                Task.FromResult(IdentityResult.FromSucceeded()) 
                : Task.FromResult(IdentityResult.Failed(new InvalidOperationException("email format is not valid.")));
        }

        public Task<IdentityResult> ValidatePassword(string password)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateUserName(string userName)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}