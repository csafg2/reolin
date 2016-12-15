using Microsoft.IdentityModel.Tokens;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtManager
    {
        string IssueJwt(TokenProviderOptions options);
        bool ValidateToken(string user, string tokenId);
        void InvalidateToken(string user, string tokenId);
        bool VerifyToken(string token, TokenValidationParameters validationParams);
    }
}