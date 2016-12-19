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
        /// <summary>
        /// an IQueryable object which can be used to query the underlying data storage
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includes">navigational properties to include in result set</param>
        /// <returns></returns>
        IQueryable<User> Query(Expression<Func<User, bool>> filter, params string[] includes);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<int> CreateAsync(User user);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        Task<int> CreateAsync(string userName, byte[] password, string email, params string[] roles);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);

        //Task<int> DeleteAsync(User user);

        /// <summary>
        /// Get a uer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// gets username by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
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
