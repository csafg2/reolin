using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using EntityFramework.Extensions;
using System.Linq.Expressions;

namespace Reolin.Web.Security.Membership
{


    public class UserSecurityStore : IUserSecurityStore<User, int>
    {
        private readonly DataContext _context;
        public UserSecurityStore(DataContext context)
        {
            this._context = context;
        }

        private DataContext Context
        {
            get
            {
                return _context;
            }
        }

        public Task CreateAsync(User user)
        {
            Context.Users.Add(user);
            return Context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            return Context.Users.Where(u => u.Id == id).DeleteAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return Context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public Task Update(User user)
        {
            if (Context.Entry(user).State == EntityState.Detached)
            {
                Context.Users.Attach(user);
            }

            return Context.SaveChangesAsync();
        }

        public Task Update(Expression<Func<User, bool>> filter, User user)
        {
            return Context.Users.Where(filter).UpdateAsync(u => user);
        }
    }
}