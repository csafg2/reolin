using EntityFramework.Extensions;
using Reolin.Data.Domain;
using Reolin.Data.Services.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Reolin.Data.Services
{
    public class UserService : IUserService, IDisposable
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            this._context = context;
        }

        private DataContext Context
        {
            get
            {
                return _context;
            }
        }

        public async Task<int> CreateAsync(User user)
        {
            if (user.Roles == null || user.Roles.Count < 1)
            {
                user.Roles = new List<Role>() { await InitializeDefaultRole() };
            }

            this.Context.Users.Add(user);
            return await this.Context.SaveChangesAsync();
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
            Role defaultRole = Role.Default();
            Role role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Name == defaultRole.Name);
            if (role == null)
            {
                role = Role.Default();
                this.Context.Roles.Add(role);
                await this.Context.SaveChangesAsync();
            }
            return role;
        }

        //public Task<int> DeleteAsync(User user)
        //{
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }
        //    return this.DeleteAsync(user.Id);
        //}

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
        
        public async Task<int> AddToRole(int userId, int roleId)
        {
            User user = await this.Context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("user could not be found");
            }

            Role role = await this.Context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null)
            {
                throw new Exception("role could not be found");
            }

            user.Roles.Add(role);
            return await Context.SaveChangesAsync();
        }

        public async Task<int> AddToRole(int userId, string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
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


        public Task<bool> UserExists(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(User));
            }

            return this.Context.Users.AnyAsync(u => u.UserName == userName);
        }


        public Task<User> GetByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return this.Context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<int> SetUserLocation(string userName, double longitude, double latitude)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (!longitude.IsValidLongitude() || !latitude.IsValideLatitude())
            {
                throw new InvalidOperationException("longitude or latitude is invalid");
            }

            DbGeography location = GeoHelpers.FromLongitudeLatitude(longitude, latitude, GeoHelpers.Geo_SRID);

            User user = await this.GetByUserName(userName, "Address");
            user.Address = user.Address ?? new Address() { Location = location };
            return await Context.SaveChangesAsync();
        }

        public Task<User> GetByUserName(string userName, params string[] includes)
        {
            return this.Query(u => u.UserName == userName, includes).FirstOrDefaultAsync();
        }

        private Task<List<User>> GetNearybyUsers(DbGeography source, double radius, string tag)
        {
            return this.Context
                .Users
                    .Where(u => u.Tags.Any(t => t.Name == tag) && u.Address.Location.Distance(source) <= radius)
                        .ToListAsync();
        }

        public Task<List<User>> GetNearybyUsers(double sourceLat, double sourceLong, double radius, string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentNullException(nameof(tag));
            }

            if (!sourceLat.IsValideLatitude() || !sourceLong.IsValidLongitude())
            {
                throw new ArgumentException("Latitude or longitude is invalid");
            }

            return this.GetNearybyUsers(GeoHelpers.FromLongitudeLatitude(sourceLong, sourceLat, GeoHelpers.Geo_SRID), radius, tag);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                this._context.Dispose();
            }
        }

        public Task<int> SetUserInfo(int userId, string firstName, string lastName)
        {
            return this.Context
                .Users
                .Where(u => u.Id == userId)
                    .UpdateAsync(u =>
                          new User()
                          {
                              FirstName = firstName,
                              LastName = lastName
                          });

        }
    }
}
