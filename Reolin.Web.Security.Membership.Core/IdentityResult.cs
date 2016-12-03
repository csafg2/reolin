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
            return Failed(new Exception());
        }
        
        public static IdentityResult Failed(Exception ex)
        {
            return new Failed() { Exception = ex };
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