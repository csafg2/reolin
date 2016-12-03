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

        public Task<int> CreateAsync(User user)
        {
            this.Context.Users.Add(user);
            return this.Context.SaveChangesAsync();
        }

        public Task<int> CreateAsync(string userName, byte[] password, string email)
        {
            return CreateAsync(new User()
            {
                UserName = userName,
                Email = email,
                Password = password
            });
        }

        public Task<int> DeleteAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return this.DeleteAsync(user.Id);
        }

        public Task<int> DeleteAsync(int id)
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
            if (Context.Entry(user).State == EntityState.Detached)
            {
                Context.Users.Attach(user);
            }

            return Context.SaveChangesAsync();
        }

        public Task<User> GetByUserName(string userName)
        {
            return this.Context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<User> GetUserTokenInfo(string userName)
        {
            return this.Query(u => u.UserName == userName, "Roles").FirstOrDefaultAsync();
        }


        public void Dispose()
        {
            this._context.Dispose();
        }

        public Task<int> AddToRole(int userId, int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddToRole(int userId, string role)
        {
            throw new NotImplementedException();
        }
    }
}
