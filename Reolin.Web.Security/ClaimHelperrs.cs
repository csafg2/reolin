using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;

namespace System
{
    public static class ClaimHelpers
    {
        public static Claim GetUsernameClaim(this List<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == "sub");
        }
    }
}
