using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserSecurityStore<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        // create
        Task CreateAsync(TUser user);

        // read
        Task<TUser> GetByIdAsync(TKey id);
        Task<TUser> GetByEmailAsync(string email);
        // update
        Task Update(TUser user);
        Task Update(Expression<Func<TUser, bool>> filter, TUser user);

        // delete
        Task Delete(TKey id);
    }
}