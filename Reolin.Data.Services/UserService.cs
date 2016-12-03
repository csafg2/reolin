using Reolin.Data.Services.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Reolin.Data.Domain;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Reolin.Data.Services
{
    public class UserService : IUserService, IDisposable
    {
        private DataContext _context;

        public UserService(DataContext context)
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
            this.Context.Users.Add(user);
            return this.Context.SaveChangesAsync();
        }

        public Task CreateAsync(string userName, byte[] password, string email)
        {
            return CreateAsync(new User()
            {
                UserName = userName,
                Email = email,
                Password = password
            });
        }

        public Task DeleteAsync(User user)
        {
            return this.DeleteAsync(user.Id);
        }

        public Task DeleteAsync(int id)
        {
            return this.Context.Users.Where(u => u.Id == id).DeleteAsync();
        }

     
        public Task<User> GetByIdAsync(int id)
        {
            return this.Context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public IQueryable<User> Query(Expression<Func<User, bool>> filter, params string[] includes)
        {
            DbQuery<User> query = this.Context.Users;
            includes?.ForEach(inc => query = query.Include(inc));
            return query.Where(filter);
        }

        public Task UpdateAsync(User user)
        {
            if (Context.Entry(user).State == EntityState.Deleted)
            {
                Context.Users.Attach(user);
            }

            return Context.SaveChangesAsync();
        }
        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
