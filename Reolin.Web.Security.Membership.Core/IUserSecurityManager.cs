using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{

    public interface IUserSecurityManager<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        IUserPasswordHasher PasswordHasher { get; set; }
        IUserSecurityStore<TUser, TKey> Store { get; }
        IEnumerable<IUserValidator<TUser, TKey>> Validators { get; }
        //Task<IdentityResult> CreateAsync(TUser user);
        Task CreateAsync(string userName, string password, string email);
        Task<IdentityResult> ValidateAsync(TUser user);
        Task ChangePasswordAsync(TKey id, string oldPassword, string newPassword);
        Task<TUser> GetUserByEmailAsync(string email);
    }
}