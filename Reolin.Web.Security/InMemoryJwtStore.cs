using System;
using System.Collections.Generic;
using System.Linq;

namespace Reolin.Web.Security.Jwt
{
    public class InMemoryJwtStore : IJwtStore
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

        public bool HasToken(string issuer, string token)
        {
            return this._store.ContainsKey(issuer) && this._store[issuer].Any(t => t == token);
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
            if (this._store.ContainsKey(key))
            {
                return this._store[key].Remove(token);
            }

            return false;
        }
    }
}