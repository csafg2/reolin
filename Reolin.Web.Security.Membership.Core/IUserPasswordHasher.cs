namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserPasswordHasher
    {
        byte[] Hash(string password);
    }
}