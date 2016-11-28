namespace Reolin.Web.Security.Membership
{




    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
    }
}