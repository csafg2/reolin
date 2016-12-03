using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using Reolin.Data.Domain;

namespace Reolin.Web.Security.Membership.Validators
{

    public class ChangePasswordValidator
    {
        public Task<IdentityResult> Validate(User user)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateChangePassword(IUserSecurityManager manager,
            User user,
            string oldPassword,
            string currentPassord)
        {
            byte[] oldPasswordHash = manager.PasswordHasher.ComputeHash(oldPassword);
            if (!Compare(oldPasswordHash, user.Password))
            {
                return Task.FromResult(IdentityResult.Failed("passwords do not match"));
            }

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

        private bool Compare(byte[] first, byte[] second)
        {
            for (int i = 0; i <= first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class PasswordLengthValidator
    {

        public Task<IdentityResult> Validate(User user)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateChangePassword(IUserSecurityManager manager,
            User user,
            string oldPassword, 
            string currentPassord)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());
        }

        public Task<IdentityResult> ValidateEmail(string email)
        {
            return Task.FromResult(IdentityResult.FromSucceeded());

        }

        public Task<IdentityResult> ValidateStringPassword(string password)
        {
            if (password.Length > 50)
            {
                return Task.FromResult(IdentityResult.Failed("password length can not be more than 50"));
            }
            if (password.Length < 6)
            {
                return Task.FromResult(IdentityResult.Failed("password length can not be less than 6"));
            }

            return Task.FromResult(IdentityResult.FromSucceeded());
        }

    }
}
