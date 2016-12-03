using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using Reolin.Data.Domain;
using Reolin.Data.Services.Core;

namespace Reolin.Web.Security.Membership
{
    //public class UserSecurityStore : IUserSecurityStore
    //{
    //    private readonly IUserService _userSrvice;

    //    public UserSecurityStore(IUserService service)
    //    {
    //        this._userSrvice = service;
    //    }

    //    private IUserService UserService
    //    {
    //        get
    //        {
    //            return _userSrvice;
    //        }
    //    }

    //    public Task CreateAsync(User user)
    //    {
    //        if (user == null)
    //        {
    //            throw new ArgumentNullException(nameof(user));
    //        }

    //        return this.UserService.CreateAsync(user);
    //    }

    //    public Task DeleteAsync(int id)
    //    {
    //        return this.UserService.DeleteAsync(id);
    //    }

    //    public Task DeleteAsync(User user)
    //    {
    //        return this.UserService.DeleteAsync(user);
    //    }

    //    public Task<User> GetByEmailAsync(string email)
    //    {
    //        return this.UserService.Query(u => u.Email == email).FirstOrDefaultAsync();
    //    }

    //    public Task<User> GetByIdAsync(int id)
    //    {
    //        return this.UserService.GetByIdAsync(id);
    //    }

    //    public Task UpdateAsync(User user)
    //    {
    //        return this.UserService.UpdateAsync(user);
    //    }
    //}
}