namespace Reolin.Web.Security.Membership.Core
{

    public class InvalidPasswordException : IdentityException
    {
        public InvalidPasswordException(string message) : base(message)
        {
        }
    }
}