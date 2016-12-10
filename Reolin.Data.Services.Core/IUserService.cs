using Reolin.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Reolin.Data.Services.Core
{
    public interface IUserService
    {
        IQueryable<User> Query(Expression<Func<User, bool>> filter, params string[] includes);
        Task<int> CreateAsync(User user);
        Task<int> CreateAsync(string userName, byte[] password, string email, params string[] roles);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(User user);
        Task<User> GetByIdAsync(int id);
        //Task<User> GetUserTokenInfo(string userName);
        Task<User> GetByUserName(string userName);
        Task<User> GetByUserName(string userName, params string[] includes);
        Task UpdateAsync(User user);
        Task<int> AddToRole(int userId, int roleId);
        Task<int> AddToRole(int userId, string role);
        Task<bool> UserExists(string userName);
        Task<User> GetByEmailAsync(string email);
        Task<int> SetUserLocation(string userName, double longitude, double latitude);
        Task<List<User>> GetNearybyUsers(double sourceLat, double sourceLong, double radius, string tag);
        Task<int> SetUserInfo(int userName, string firstName, string lastName);
    }
}
