namespace Reolin.Web.Security.Jwt
{
    public interface IJWTManager
    {
        string IssueJwt(TokenProviderOptions options);
        bool ValidateToken(string issuer, string token);
        void InvalidateToken(string issuer, string token);
    }
}