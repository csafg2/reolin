using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserSecurityManager
    {
        IUserPasswordHasher PasswordHasher { get; }
        IEnumerable<IUserValidator> Validators { get; }
        Task<int> CreateAsync(string userName, string password, string email);
        Task<IdentityResult> ValidateUserPasswordAsync(string userName, string password);
        Task ChangePasswordAsync(int id, string oldPassword, string newPassword);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);

        /// <summary>
        /// gets login info for specified user (roles and and stuff like that)
        /// remember that all methods with "IdentityResult" return type must not throw Exception directly
        /// it has to be wrapped by result.Exception
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IdentityResult> GetLoginInfo(string userName, string password);
    }
}