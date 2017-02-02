using Reolin.Data.Domain;
using Reolin.Data.DTO;
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

        /// <summary>
        /// retrieve a user object by userName
        /// </summary>
        /// <param name="userName">username field of the user</param>
        /// <param name="includes">navigational properties to include in query</param>
        /// <returns></returns>
        Task<User> GetByUserName(string userName, params string[] includes);

        /// <summary>
        /// Updates a user object
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// add a user to a role group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId">the roleId in which the user should be added</param>
        /// <returns></returns>
        Task<int> AddToRole(int userId, int roleId);

        /// <summary>
        /// add a user to a role group
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId">the role name in which the user should be added</param>
        /// <returns></returns>
        Task<int> AddToRole(int userId, string role);

        /// <summary>
        /// checks weather a user exists
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>true if user exists otherwise false</returns>
        Task<bool> UserExists(string userName);

        /// <summary>
        /// retrieve a user object by it`s email field
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Updates the user location to the specified lat and long
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <returns> <1 if successfully updated</returns>
        Task<int> SetUserLocation(string userName, double longitude, double latitude);
        
        /// <summary>
        /// retrieve all users within the radius range specified
        /// </summary>
        /// <param name="sourceLat"></param>
        /// <param name="sourceLong"></param>
        /// <param name="radius"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        Task<List<User>> GetNearybyUsers(double sourceLat, double sourceLong, double radius, string tag);

        
        Task<int> SetUserInfo(int userName, string firstName, string lastName);


        /// <summary>
        /// Gets all profile entries that are attached to this userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<ProfileInfoDTO>> QueryProfiles(int userId);
    }
}
