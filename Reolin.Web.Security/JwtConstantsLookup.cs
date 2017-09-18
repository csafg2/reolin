namespace Reolin.Web.Security.Jwt
{
    public static class JwtConstantsLookup
    {
        public const string ROLE_VALUE_TYPE = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public const string INT_VALUE_TYPE = "http://www.w3.org/2001/XMLSchema#integer";
        public const string USERNAME_SCHEMA = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public const string Role_CLAIM_TYPE = "roles";
        public const string TOKEN_SCHEME = "Bearer";
        public const string HEADER_KEY = "Authorization";
        public const string ID_CLAIM_TYPE = "Id";
        public const string USERNAME_CLAIM_TYPE = "sub";
        public const string JWT_JTI_TYPE = "jti";
        public const string PROFILE_CLAIM_TYPE = "p";
    }
}