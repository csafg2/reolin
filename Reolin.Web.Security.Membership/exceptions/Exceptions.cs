using System;

namespace Reolin.Web.Security.Membership.exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message)
        {
            this._message = message;
        }

        private string _message;

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }

    public class PasswordNotValidException: Exception
    {
        private string _message;

        public PasswordNotValidException(string message)
        {
            this._message = message;
        }
        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
    
}
