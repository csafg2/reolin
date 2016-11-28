using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reolin.Web.Security.Membership.Core;

namespace Reolin.Web.Security.Membership
{
    public class UserManager<TUser, TKey> : IUserSecurityManager<TUser, TKey>
        where TUser : IUser<TKey>, new()
        where TKey : struct
    {
        private readonly IUserSecurityStore<TUser, TKey> _store;
        private readonly IEnumerable<IUserValidator<TUser, TKey>> _validators;

        public UserManager(IUserSecurityStore<TUser, TKey> userStore,
            IEnumerable<IUserValidator<TUser, TKey>> validators)
        {
            _store = userStore;
            _validators = validators;
        }

        public IUserPasswordHasher PasswordHasher { get; set; }
        public IUserSecurityStore<TUser, TKey> Store { get { return _store; } }
        public IEnumerable<IUserValidator<TUser, TKey>> Validators { get { return _validators; } }

        public async Task ChangePasswordAsync(TKey id, string oldPassword, string newPassword)
        {
            TUser user = await this.Store.GetByIdAsync(id);
            foreach (var validator in this.Validators)
            {
                await validator.ValidateChangePassword(this, user, oldPassword, newPassword);
            }

            user.Password = this.PasswordHasher.ComputeHash(newPassword);
            await Store.Update(user);
        }

        private Task CreateAsync(TUser user)
        {
            return this.Store.CreateAsync(user);
        }

        public async Task CreateAsync(string userName, string password, string email)
        {
            foreach (var item in this.Validators)
            {
                await item.ValidateStringPassword(password);
            }

            await CreateAsync(new TUser()
            {
                UserName = userName,
                Password = this.PasswordHasher.ComputeHash(password),
                Email = email
            });
        }

        public Task<TUser> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }

            return Store.GetByEmailAsync(email);
        }

        public Task<IdentityResult> ValidateAsync(TUser user)
        {
            foreach (var validator in this.Validators)
            {
                validator.Validate(user);
            }

            return Task.FromResult(IdentityResult.FromSucceeded());
        }
    }
}