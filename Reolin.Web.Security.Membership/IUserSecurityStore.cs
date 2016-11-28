using System;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Reolin.Web.Security.Membership
{


    public interface IUserSecurityStore<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        // create
        Task CreateAsync(TUser user);

        // read
        Task<TUser> GetByIdAsync(TKey id);

        // update
        Task Update(TUser user);
        Task Update(Expression<Func<User, bool>> filter, User user);

        // delete
        Task Delete(TKey id);
    }
}