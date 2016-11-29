using Reolin.Domain;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{

    public interface IUserValidator
    {
        Task<IdentityResult> Validate(User user);
        Task<IdentityResult> ValidateChangePassword(IUserSecurityManager manager,
            User user,
            string oldPassword,
            string currentPassord);

        Task<IdentityResult> ValidateStringPassword(string password);
        Task<IdentityResult> ValidateEmail(string email);
    }
}