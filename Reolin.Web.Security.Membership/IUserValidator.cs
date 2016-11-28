using System.Threading.Tasks;

namespace Reolin.Web.Security.Membership
{

    public interface IUserValidator<TUser, TKey> where TUser : IUser<TKey> where TKey : struct
    {
        Task<IdentityResult> Validate(TUser user);
        ValidatorType Type { get; set; }
    }
}