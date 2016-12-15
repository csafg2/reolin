using System;
using System.Collections.Generic;
using System.Linq;

namespace Reolin.Web.Security.Jwt
{
    public class InMemoryJwtStore : IJwtStore
    {
        private readonly Dictionary<string, List<string>> _store = new Dictionary<string, List<string>>();

        public void Add(string key, string tokenId)
        {
            if (_store.ContainsKey(key))
            {
                _store[key].Add(tokenId);
                return;
            }

            _store[key] = new List<string>() { tokenId };
        }

        public bool HasToken(string issuer, string tokenId)
        {
            return this._store.ContainsKey(issuer) && this._store[issuer].Any(t => t == tokenId);
        }

        public void Remove(string key, string tokenId)
        {
            if (!_store.ContainsKey(key))
            {
                throw new KeyNotFoundException($"the key {key} could not be found");
            }

            else if (!_store[key].Any(t => t == tokenId))
            {
                throw new Exception($"tokenId {tokenId} could not be found ");
            }

            this._store[key].Remove(tokenId);
        }
    }
}