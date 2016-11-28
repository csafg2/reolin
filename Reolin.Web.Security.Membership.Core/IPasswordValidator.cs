using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{

    public interface IPasswordValidator<TUser, TKey> :
        IUserValidator<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        Task<IdentityResult> ValidateChangePassword(IUserSecurityManager<TUser, TKey> manager,
            TUser user,
            string oldPassword, 
            string currentPassord);

        Task ValidateStringPassword(string password);
    }
}