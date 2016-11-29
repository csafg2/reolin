using Reolin.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface IUserService
    {
        IQueryable<User> Query(Expression<Func<User, bool>> filter, params string[] includes);
        Task CreateAsync(User user);
        Task CreateAsync(string userName, byte[] password, string email);
        Task DeleteAsync(int id);
        Task DeleteAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User user);
    }
}
