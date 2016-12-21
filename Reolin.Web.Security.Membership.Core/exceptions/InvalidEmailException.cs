namespace Reolin.Web.Security.Membership.Core
{


    public class InvalidEmailException : IdentityException
    {
        public InvalidEmailException(string message) : base(message)
        {
        }
    }
}