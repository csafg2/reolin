using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtManager
    {
        /// <summary>
        /// produce a jwt with specified options, the specified jwt will be tracked by the manager
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        string IssueJwt(TokenProviderOptions options);

        /// <summary>
        /// validates the token aginst the underlying IJWTStore
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        bool ValidateToken(string user, string tokenId);

        /// <summary>
        /// invlidates token against the underlying JwtStore
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tokenId"></param>
        void InvalidateToken(string user, string tokenId);


        /// <summary>
        /// verifies token signature, exp, etc...
        /// </summary>
        /// <param name="token">token to be verified</param>
        /// <param name="validationParams"></param>
        /// <returns></returns>
        bool VerifyToken(string token, TokenValidationParameters validationParams);

        /// <summary>
        /// produce a token in exchange with an old token which is valid but expired.
        /// </summary>
        /// <param name="oldToken">expired token</param>
        /// <returns>newly created jwt</returns>
        string ExchangeToken(JwtSecurityToken oldToken);
    }
}