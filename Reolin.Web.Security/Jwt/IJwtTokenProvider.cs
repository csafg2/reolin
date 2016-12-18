
using System.IdentityModel.Tokens.Jwt;

namespace Reolin.Web.Security.Jwt
{
    /// <summary>
    /// the base provider interface for generating JWTS
    /// </summary>
    public interface IJwtProvider
    {
        /// <summary>
        /// Create a jwt and write to a string.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        string ProvideJwt(TokenProviderOptions options);

        /// <summary>
        /// Creates a JwtSecurityToken for specified option
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        JwtSecurityToken CreateJwt(TokenProviderOptions options);

        /// <summary>
        /// converts a JWT to a encodedString
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        string JwtToString(JwtSecurityToken jwt);
    }
}
