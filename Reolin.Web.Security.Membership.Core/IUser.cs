namespace Reolin.Web.Security.Membership.Core
{
    public interface IUser
    {
        int Id { get; }
        string UserName { get; set; }
        string Email { get; set; }
        byte[] Password { get; set; }
    }
}