namespace Reolin.Web.Security.Membership.Core
{

    public class InvalidUserNameException : IdentityException
    {
        public InvalidUserNameException(string message) : base(message)
        {
        }
    }
}