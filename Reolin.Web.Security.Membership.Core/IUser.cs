namespace Reolin.Web.Security.Membership.Core
{

    public interface IUser<TKey> where TKey : struct
    {
        TKey Id { get; }
        string UserName { get; set; }
        string Email { get; set; }
        byte[] Password { get; set; }
    }
}