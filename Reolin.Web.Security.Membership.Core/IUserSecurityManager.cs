using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserSecurityManager
    {
        IUserPasswordHasher PasswordHasher { get; }
        IEnumerable<IUserValidator> Validators { get; }
        Task CreateAsync(string userName, string password, string email);
        Task<IdentityResult> ValidateUserPasswordAsync(string userName, string password);
        Task ChangePasswordAsync(int id, string oldPassword, string newPassword);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
        Task<User> GetLoginInfo(string userName, string password);
    }
}