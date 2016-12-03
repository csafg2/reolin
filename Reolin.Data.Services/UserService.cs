using Reolin.Data.Services.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using Reolin.Data.Domain;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Collections.Generic;

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
            if (user.Roles == null || user.Roles.Count < 1)
            {
                throw new InvalidOperationException("user must have at least one role");
            }

            this.Context.Users.Add(user);
            return this.Context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(string userName, byte[] password, string email, params string[] roles)
        {
            User user = new User()
            {
                UserName = userName,
                Email = email,
                Password = password
            };
            if (roles != null && roles.Length > 0)
            {
                user.Roles = new List<Role>() { await InitializeDefaultRole() };
            }

            return await CreateAsync(user);
        }

        private async Task<Role> InitializeDefaultRole()
        {
            Role role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Name == Role.Default().Name);
            if (role == null)
            {
                role = Role.Default();
                this.Context.Roles.Add(role);
                await this.Context.SaveChangesAsync();
            }
            return role;
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
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (Context.Entry(user).State == EntityState.Detached)
            {
                Context.Users.Attach(user);
            }

            return Context.SaveChangesAsync();
        }

        public Task<User> GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }
                
            return this.Context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<User> GetUserTokenInfo(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return this.Context.Users.Include("Roles").FirstOrDefaultAsync(u => u.UserName == userName);
        }


        public async Task<int> AddToRole(int userId, int roleId)
        {
            User user = await this.Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if(user == null)
            {
                throw new Exception("user could not be found");
            }

            Role role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if(role == null)
            {
                throw new Exception("role could not be found");
            }

            user.Roles.Add(role);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddToRole(int userId, string roleName)
        {
            if(string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException(roleName);
            }

            Role role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            if (role == null)
            {
                throw new Exception("role could not be found");
            }

            User user = await this.Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.Roles.Add(role);

            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }

    }
}
