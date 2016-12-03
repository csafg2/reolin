using Reolin.Data.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{

    public interface IUserSecurityManager
    {
        IUserPasswordHasher PasswordHasher { get; set; }
        IEnumerable<IUserValidator> Validators { get; }
        Task CreateAsync(string userName, string password, string email);
        Task<IdentityResult> ValidateAsync(User user);
        Task ChangePasswordAsync(int id, string oldPassword, string newPassword);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
    }
}