using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;
using Reolin.Data.Services.Core;
using Reolin.Data.Domain;
using System.Data.Entity;
using System.Text;
using Reolin.Web.Security.Membership.exceptions;

namespace Reolin.Web.Security.Membership
{
    public class UserSecurityManager : IUserSecurityManager
    {
        private readonly IEnumerable<IUserValidator> _validators;
        private readonly IUserService _service;
        private readonly IUserPasswordHasher _passwordHasher;

        public UserSecurityManager(IUserService service,
            IEnumerable<IUserValidator> validators, IUserPasswordHasher passwordhasher)
        {
            this._service = service;
            this._validators = validators;
            this._passwordHasher = passwordhasher;
        }

        public IUserPasswordHasher PasswordHasher { get { return _passwordHasher; } }
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
            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(oldPassword))
            {
                throw new ArgumentException("old password and new password are required");
            }
            
            foreach (var validator in this.Validators)
            {
                IdentityResult result = await validator.ValidatePassword(newPassword);
                IdentityResult oldPass = await validator.ValidatePassword(oldPassword);

                if (!result.Succeeded)
                {
                    throw result.Exception;
                }
                else if (!oldPass.Succeeded)
                {
                    throw oldPass.Exception;
                }
            }

            User user = await this.UserService.GetByIdAsync(id);
            user.Password = this.PasswordHasher.ComputeHash(newPassword);
            await this.UserService.UpdateAsync(user);
        }

        private async Task<int> CreateAsync(User user)
        {
            return await this.UserService.CreateAsync(user);
        }

        public async Task<int> CreateAsync(string userName, string password, string email)
        {
            foreach (var item in this.Validators)
            {
                IdentityResult passwordValidation = await item.ValidatePassword(password);
                IdentityResult userNameValidation = await item.ValidateUserName(userName);
                IdentityResult emailValidation = await item.ValidateEmail(email);

                if (!passwordValidation.Succeeded)
                {
                    throw passwordValidation.Exception;
                }
                else if (!userNameValidation.Succeeded)
                {
                    throw userNameValidation.Exception;
                }
                else if (!emailValidation.Succeeded)
                {
                    throw emailValidation.Exception;
                }
            }

            if ((await this.UserService.UserExists(userName)))
            {
                throw new UserNameTakenExistsException("this username is already taken.");
            }
            else if ((await this.UserService.GetByEmailAsync(email)) != null)
            {
                throw new EmailTakenException("this email is taken");
            }

            return await CreateAsync(new User()
            {
                UserName = userName,
                Password = this.PasswordHasher.ComputeHash(password),
                Email = email
            });
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            foreach (var item in this.Validators)
            {
                IdentityResult result = await item.ValidateEmail(email);
                if (!result.Succeeded)
                {
                    throw result.Exception;
                }
            }
            return await UserService.Query(u => u.Email == email).FirstOrDefaultAsync();
        }

        // check the username and password provided
        public async Task<IdentityResult> ValidateUserPasswordAsync(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("username of password is required.");
            }

            User user = await this._service.GetByUserName(userName);
            if (user == null)
            {
                return IdentityResult.Failed(new UserNotFoundException($"username {userName} dose not exist."));
            }
            else if (!user.Password.IsEqualTo(Encoding.UTF8.GetBytes(password)))
            {
                return IdentityResult.Failed(new PasswordNotValidException("Password is not valid."));
            }

            return IdentityResult.FromSucceeded();
        }

        public Task<User> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException(nameof(userName));
            }

            return this._service.Query(user => user.UserName == user.UserName).FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> GetLoginInfo(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {

                throw new ArgumentNullException("username and password are both required");
            }

            User user = null;
            try
            {
                user = await this.UserService.GetByUserName(userName, "Roles");
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(ex, IdentityResultErrors.EmptyOrUnknown);
            }

            if (user == null)
            {
                return IdentityResult.Failed(new Exception($"{userName} could not be found"), IdentityResultErrors.UserNotFound);
            }

            if (!user.Password.IsEqualTo(this.PasswordHasher.ComputeHash(password)))
            {
                return IdentityResult.Failed(new Exception("Password is invalid"), IdentityResultErrors.InvalidPassowrd);
            }

            return IdentityResult.FromSucceeded(user);
        }
    }
}