using Reolin.Data.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface IUserService
    {
        IQueryable<User> Query(Expression<Func<User, bool>> filter, params string[] includes);
        Task<int> CreateAsync(User user);
        Task<int> CreateAsync(string userName, byte[] password, string email);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserTokenInfo(string userName);
        Task<User> GetByUserName(string userName);
        Task UpdateAsync(User user);
        Task<int> AddToRole(int userId, int roleId);
        Task<int> AddToRole(int userId, string role);
    }
}
