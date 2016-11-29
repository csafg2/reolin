using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using Reolin.Data.Services.Core;
using Reolin.Domain;
using System.Data.Entity;

namespace Reolin.Web.Security.Membership
{
    public class UserManager: IUserSecurityManager
    {
        private readonly IEnumerable<IUserValidator> _validators;
        private readonly IUserService _service;

        public UserManager(IUserService service, IEnumerable<IUserValidator> validators)
        {
            this._service = service;
            this._validators = validators;
        }

        public IUserPasswordHasher PasswordHasher { get; set; }
        public IEnumerable<IUserValidator> Validators { get { return _validators; } }

        private IUserService UserService
        {
            get
            {
                return _service;
            }
        }
     
        public async Task ChangePasswordAsync(int id, string oldPassword, string newPassword)
        {
            User user = await this.UserService.GetByIdAsync(id);
            foreach (var validator in this.Validators)
            {
                IdentityResult result = await validator.ValidateChangePassword(this, user, oldPassword, newPassword);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(result.Message);
                }
            }

            user.Password = this.PasswordHasher.ComputeHash(newPassword);
            await this.UserService.UpdateAsync(user);
        }

        private Task CreateAsync(User user)
        {
            return this.UserService.CreateAsync(user);
        }

        public async Task CreateAsync(string userName, string password, string email)
        {
            foreach (var item in this.Validators)
            {
                await item.ValidateStringPassword(password);
            }

            await CreateAsync(new User()
            {
                UserName = userName,
                Password = this.PasswordHasher.ComputeHash(password),
                Email = email
            });
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return UserService.Query(u => u.Email == email).FirstOrDefaultAsync();
        }

        public Task<IdentityResult> ValidateAsync(User user)
        {
            foreach (var validator in this.Validators)
            {
                validator.Validate(user);
            }

            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}