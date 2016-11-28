using System;

namespace Reolin.Web.Security.Membership.Core
{

    public abstract class IdentityResult
    {
        public abstract bool Succeeded { get; }
        public string Message { get; set; }

        public Exception Exception { get; set; }
        public static IdentityResult Failed()
        {
            return Failed(string.Empty);
        }

        public static IdentityResult Failed(string message)
        {
            return new Failed() { Message = message };
        }

        public static IdentityResult FromSucceeded(string message)
        {
            return new SucceedResult() { Message = message };
        }
        public static IdentityResult FromSucceeded()
        {
            return FromSucceeded(string.Empty);
        }
    }
}