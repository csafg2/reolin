using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using System.Text.RegularExpressions;
using Reolin.Data.Domain;

namespace Reolin.Web.Security.Membership.Validators
{
    public class UserEmailValidator
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
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            return regex.Match(email).Success ? 
                Task.FromResult(IdentityResult.FromSucceeded()) 
                : Task.FromResult(IdentityResult.Failed("email is not valid"));
        }

        public Task<IdentityResult> ValidateStringPassword(string password)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}