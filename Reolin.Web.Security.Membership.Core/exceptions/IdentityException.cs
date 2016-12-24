using System;

namespace Reolin.Web.Security.Membership.Core
{

    public class IdentityException: Exception
    {
        public IdentityException(string message): base(message)
        {

        }
    }
}