using Reolin.Data.Domain;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserValidator
    {
        Task<IdentityResult> ValidateUserName(string userName);
        Task<IdentityResult> ValidatePassword(string password);
        Task<IdentityResult> ValidateEmail(string email);
    }
}