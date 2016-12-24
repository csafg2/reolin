using Reolin.Web.Security.Membership.Core;

namespace Reolin.Web.Security.Membership.exceptions
{
    public class EmailTakenException : IdentityException
    {
        public EmailTakenException(string message) : base(message)
        {

        }
    }

    public class UserNameTakenExistsException : IdentityException
    {
        public UserNameTakenExistsException(string message) : base(message)
        {

        }
    }

    public class UserNotFoundException : IdentityException
    {
        public UserNotFoundException(string message) : base(message)
        {
        }
    }

    public class PasswordNotValidException : IdentityException
    {

        public PasswordNotValidException(string message) : base(message)
        {
        }
    }
}
