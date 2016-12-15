using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace System
{
    public static class ClaimHelpers
    {
        const string userNameType = "sub";
        const string secondUserNameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        public static Claim GetUsernameClaim(this IEnumerable<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == userNameType || c.Type == secondUserNameType);
        }
    }
}
