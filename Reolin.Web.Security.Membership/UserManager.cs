using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Reolin.Web.Security.Membership.Core;

namespace Reolin.Web.Security.Membership
{
    public class UserManager<TUser, TKey> : IUserSecurityManager<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
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

        public IEnumerable<IUserValidator<TUser, TKey>> Validators
        {
            get
            {
                return _validators;
            }
        }

        public async Task<IdentityResult> ChangePasswordAsync(TKey id, string oldPassword, string newPassword)
        {
            TUser user = await this.Store.GetByIdAsync(id);
            byte[] oldPasswordHash = this.PasswordHasher.Hash(oldPassword);
            byte[] currentPasswordHash = this.PasswordHasher.Hash(newPassword);

            foreach (var item in this.Validators.Where(v => v.Type == ValidatorType.PasswordValidator))
            {
                IPasswordValidator<TUser, TKey> validator = (IPasswordValidator<TUser, TKey>)item;
                IdentityResult result = await validator.ValidateChangePassword(oldPassword, newPassword);
                if(!result.Succeeded)
                {
                    return result;
                }
            }

            return IdentityResult.FromSucceeded();
        }

        public Task<IdentityResult> CreateAsync(TUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(string userName, string password, string email)
        {
            throw new NotImplementedException();
        }

        public Task<TUser> GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ValidateAsync(TUser user)
        {
            throw new NotImplementedException();
        }
    }
}