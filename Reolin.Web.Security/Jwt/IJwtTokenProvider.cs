
using System.IdentityModel.Tokens.Jwt;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtProvider
    {
        string ProvideJwt(TokenProviderOptions options);
        JwtSecurityToken CreateJwt(TokenProviderOptions options);
        string JwtToString(JwtSecurityToken jwt);
    }
}
