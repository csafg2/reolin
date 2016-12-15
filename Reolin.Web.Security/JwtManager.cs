using System.IdentityModel.Tokens.Jwt;
using System;

namespace Reolin.Web.Security.Jwt
{
    public class JwtManager : IJwtManager
    {
        IJwtStore _store;
        IJwtProvider _provider;

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
            return _store.HasToken(user, tokenId);
        }
    }
}