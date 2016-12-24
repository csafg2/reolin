using System.IdentityModel.Tokens.Jwt;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;

namespace Reolin.Web.Security.Jwt
{
    public class JwtManager : IJwtManager
    {
        private readonly IJwtStore _store;
        private readonly IJwtProvider _provider;

        public JwtManager(IJwtStore store, IJwtProvider jwtProvider)
        {
            this._store = store;
            this._provider = jwtProvider;
        }

        public void InvalidateToken(string user, string tokenId)
        {
            _store.Remove(user, tokenId);
        }

        public string IssueJwt(TokenProviderOptions options)
        {
            JwtSecurityToken token = _provider.CreateJwt(options);
            _store.Add(options.Claims.GetUsernameClaim().Value, token.Id);
            return _provider.JwtToString(token);
        }

        public bool ValidateToken(string user, string tokenId)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentException("user or tokenId can not be null or empty");
            }

            return _store.HasToken(user, tokenId);
        }

        public bool VerifyToken(string token, TokenValidationParameters validationParams)
        {
            validationParams.ValidateLifetime = false;

            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken = null;
                tokenHandler.ValidateToken(token, validationParams, out validatedToken);
            }
            catch (Exception)
            {
                // fucking idiots at Microsoft are controlling program flow by throwing exceptions
                // so if verification fails an exception will be thrown.
                return false;
            }

            return true;
        }

        public string ExchangeToken(JwtSecurityToken oldToken)
        {
            string userName = oldToken.Claims.ToList().GetUsernameClaim().Value;

            // verify token signature
            this.VerifyToken(oldToken.RawData, JwtConfigs.ValidationParameters);

            // we dont exchange a token that is already invalidated with a valid token.
            if (!this.ValidateToken(userName, oldToken.Id))
            {
                throw new SecurityTokenInvalidTokenException("the jwt is already Invalidated");
            }

            this.InvalidateToken(userName, oldToken.Id);
            TokenProviderOptions options = CreateOptions(oldToken);

            return this.IssueJwt(options);
        }

        private static TokenProviderOptions CreateOptions(JwtSecurityToken oldToken)
        {
            var options = new TokenProviderOptions()
            {
                Audience = JwtConfigs.Audience,
                Issuer = JwtConfigs.Issuer,
                SigningCredentials = JwtConfigs.SigningCredentials,
                Expiration = JwtConfigs.Expiry,
                Claims = oldToken.Claims.ToList()
            };

            //remove old id
            options.Claims.Remove(options.Claims.First(c => c.Type == JwtConstantsLookup.JWT_JTI_TYPE));

            // set a new id for the jwt
            options.Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            return options;
        }
    }
}