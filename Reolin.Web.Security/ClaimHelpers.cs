using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using static Reolin.Web.Security.Jwt.JwtConstantsLookup;

namespace System
{
    public static class ClaimHelpers
    {
        public static string GetRoles(this IEnumerable<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == ROLE_VALUE_TYPE).Value;
        }

        public static Claim GetUsernameClaim(this List<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == USERNAME_CLAIM_TYPE);
        }

        public static Claim GetUsernameClaim(this IEnumerable<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == USERNAME_CLAIM_TYPE || c.Type == USERNAME_SCHEMA);
        }

        public static Claim GetIdClaim(this IEnumerable<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == ID_CLAIM_TYPE);
        }

        public static Claim GetProfileIdsClaim(this IEnumerable<Claim> source)
        {
            return source.FirstOrDefault(c => c.Type == PROFILE_CLAIM_TYPE);
        }
    }
}
