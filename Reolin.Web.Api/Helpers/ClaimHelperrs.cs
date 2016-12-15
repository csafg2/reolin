using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace System
{
    public static class ClaimHelpers
    {
        public static string UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public static Claim GetUsernameClaim(this List<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == UserNameClaimType);
        }
    }
}
