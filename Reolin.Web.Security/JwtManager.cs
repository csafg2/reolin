using System.IdentityModel.Tokens.Jwt;

namespace Reolin.Web.Security.Jwt
{
    public class JwtManager : IJWTManager
    {
        IJwtStore _store;
        IJwtProvider _provider;

        public JwtManager(IJwtStore store, IJwtProvider jwtProvider)
        {
            this._store = store;
            this._provider = jwtProvider;
        }

        public void InvalidateToken(string issuer, string tokenId)
        {
            _store.Remove(issuer, tokenId);
        }

        public string IssueJwt(TokenProviderOptions options)
        {
            JwtSecurityToken token = _provider.CreateJwt(options);
            _store.Add(options.Issuer, token.Id);
            return _provider.JwtToString(token);
        }

        public bool ValidateToken(string issuer, string tokenId)
        {
            return _store.HasToken(issuer, tokenId);
        }
    }
}