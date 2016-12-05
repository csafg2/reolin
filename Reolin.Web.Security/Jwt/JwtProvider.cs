using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Reolin.Web.Security.Jwt
{
    public class InMemoryJwtStore : IJwtSore
    {
        private readonly Dictionary<string, List<string>> _store = new Dictionary<string, List<string>>();

        public void Add(string key, string token)
        {
            if (_store.ContainsKey(key))
            {
                _store[key].Add(token);
                return;
            }

            _store[key] = new List<string>() { token };
        }

        public List<string> Get(string key)
        {
            return _store[key];
        }

        public void Remove(string key, string jwt)
        {
            if (!_store.ContainsKey(key))
            {
                throw new KeyNotFoundException($"the key {key} could not be found");
            }
            else if (!_store[key].Any(t => t == jwt))
            {
                throw new Exception($"Token {jwt} could not be found ");
            }

            this._store[key].Remove(jwt);
        }

        public bool TryRemove(string key, string token)
        {
            try
            {
                this.Remove(key, token);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
    public interface IJWTManager
    {
        string PublishJwt(TokenProviderOptions options);
        bool ValidateJwt(string jwt);
        void InvalidateJwt(string sub, JwtSecurityToken jwt);
    }

    public class DefualtJWTManager : IJWTManager
    {
        IJwtSore _store;
        IJwtProvider _provider;

        public DefualtJWTManager(IJwtSore store)
        {
            this._store = store;
        }

        public string PublishJwt(TokenProviderOptions options)
        {
            JwtSecurityToken jwt = this._provider.CreateJwt(options);
            this._store.Add(jwt.Payload.Sub, jwt);
            return _provider.JwtToString(jwt);
        }

        public bool ValidateJwt(string jwt)
        {
            throw new NotImplementedException();
        }

        public void InvalidateJwt(string sub, JwtSecurityToken jwt)
        {
            throw new NotImplementedException();

        }
    }

    public class JwtProvider : IJwtProvider
    {
        public JwtSecurityToken CreateJwt(TokenProviderOptions options)
        {
            string sub = options.Claims?.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (string.IsNullOrEmpty(sub))
            {
                throw new ArgumentNullException("sub '(username)' claim can not be null");
            }

            DateTime now = DateTime.UtcNow;
            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: options.Claims,
                notBefore: now,
                expires: now.Add(options.Expiration),
                signingCredentials: options.SigningCredentials);

            return jwt;
        }

        public string JwtToString(JwtSecurityToken jwt)
        {
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string ProvideJwt(TokenProviderOptions options)
        {
            return new JwtSecurityTokenHandler().WriteToken(this.CreateJwt(options));
        }
    }
}