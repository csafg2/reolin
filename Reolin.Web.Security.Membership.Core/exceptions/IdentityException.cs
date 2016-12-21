using System;

namespace Reolin.Web.Security.Membership.Core
{

    public class IdentityException: Exception
    {
        public IdentityException(string message)
        {

        }
        protected string _message;
        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}