using Reolin.Data.Domain;
using System;

namespace Reolin.Web.Security.Membership.Core
{


    public abstract class IdentityResult
    {
        public abstract bool Succeeded { get; }

        public User User { get; set; }
        public Exception Exception { get; set; }
        public IdentityResultErrors Error { get; set; }

        private string _message;
        public string Message
        {
            get
            {
                if (string.IsNullOrEmpty(_message))
                {
                    return this.Exception?.Message;
                }

                return _message;
            }
            set
            {
                _message = value;
            }
        }

        public static IdentityResult Failed()
        {
            return Failed(new Exception());
        }

        public static IdentityResult Failed(Exception ex, IdentityResultErrors errorType)
        {
            return new Failed() { Exception = ex, Error = errorType };
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

        public static IdentityResult FromSucceeded(User user)
        {
            var result = FromSucceeded();
            result.User = user;
            return result;
        }
    }
}