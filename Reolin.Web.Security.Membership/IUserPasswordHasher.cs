namespace Reolin.Web.Security.Membership
{
    public interface IUserPasswordHasher
    {
        byte[] Hash(string password);
    }
}