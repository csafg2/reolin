using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtManager
    {
        string IssueJwt(TokenProviderOptions options);
        bool ValidateToken(string user, string tokenId);
        void InvalidateToken(string user, string tokenId);
        bool VerifyToken(string token, TokenValidationParameters validationParams);
        string ExchangeToken(JwtSecurityToken oldToken);
    }
}