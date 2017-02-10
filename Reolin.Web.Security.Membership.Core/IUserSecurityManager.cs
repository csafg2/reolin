using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{
    /// <summary>
    /// defines security service operations for a user account
    /// iff the method result is Identity Result exceptions will be wrapped in error property otherwise
    /// this class will always throw subclasses of IdentityException which is safe to be serialized to client
    /// </summary>
    public interface IUserSecurityManager
    {

        /// <summary>
        /// gets password hasher which will be used to hash user password before persisting it.
        /// </summary>
        IUserPasswordHasher PasswordHasher { get; }

        /// <summary>
        /// an IEnumerble of validators which will validate various user fields before storing it.
        /// </summary>
        IEnumerable<IUserValidator> Validators { get; }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userName">the new user`s username</param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<int> CreateAsync(string userName, string password, string email);

        /// <summary>
        /// validates the specified password for the username
        /// </summary>
        /// <param name="userName">ther username of the user</param>
        /// <param name="password">password string to checked if it matches</param>
        /// <returns></returns>
        Task<IdentityResult> ValidateUserPasswordAsync(string userName, string password);

        /// <summary>
        /// asynchronously changes the user`s password
        /// </summary>
        /// <param name="id"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        Task ChangePasswordAsync(int id, string oldPassword, string newPassword);

        /// <summary>
        /// get the user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetUserByEmailAsync(string email);

        /// <summary>
        /// get a user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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

        Task<int> CreateAsync(string userName, string password);
    }
}