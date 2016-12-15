namespace Reolin.Web.Security.Jwt
{
    public interface IJwtManager
    {
        string IssueJwt(TokenProviderOptions options);
        bool ValidateToken(string user, string token);
        void InvalidateToken(string user, string token);
    }
}