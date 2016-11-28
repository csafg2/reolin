namespace Reolin.Web.Security.Membership
{

    public interface IUser<TKey> where TKey : struct
    {
        TKey Id { get; }
        string UserName { get; set; }
        byte[] Password { get; set; }
    }
}