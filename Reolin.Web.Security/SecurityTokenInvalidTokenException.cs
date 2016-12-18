using System;

namespace Reolin.Web.Security
{
    public class SecurityTokenInvalidTokenException: Exception
    {
        public SecurityTokenInvalidTokenException(string message)
        {
            this._message = message;
        }

        string _message;
        public override string Message
        {
            get
            {
                return _message;
            }
        }

    }
}
