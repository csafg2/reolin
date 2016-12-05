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

        public void InvalidateToken(string issuer, string token)
        {
            _store.Remove(issuer, token);
        }

        public string IssueJwt(TokenProviderOptions options)
        {
            string jwt = _provider.ProvideJwt(options);
            _store.Add(options.Issuer, jwt);
            return jwt;
        }

        public bool ValidateToken(string issuer, string token)
        {
            return _store.HasToken(issuer, token);
        }
    }
}