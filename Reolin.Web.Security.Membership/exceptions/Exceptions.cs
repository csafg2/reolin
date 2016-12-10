using System;

namespace Reolin.Web.Security.Membership.exceptions
{
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
