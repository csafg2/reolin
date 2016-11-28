using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{

    public interface IPasswordValidator<TUser, TKey> :
        IUserValidator<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        Task<IdentityResult> ValidateChangePassword(string oldPassword, string currentPassord);
    }
}