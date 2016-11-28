namespace Reolin.Web.Security.Membership.Core
{
    public interface IUserPasswordHasher
    {
        byte[] ComputeHash(string password);
    }
}