using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{

    public interface IUserValidator<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        Task<IdentityResult> Validate(TUser user);
        Task<IdentityResult> ValidateChangePassword(IUserSecurityManager<TUser, TKey> manager,
            TUser user,
            string oldPassword,
            string currentPassord);

        Task<IdentityResult> ValidateStringPassword(string password);
        Task<IdentityResult> ValidateEmail(string email);
    }
}