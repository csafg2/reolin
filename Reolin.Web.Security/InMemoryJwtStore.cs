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

        public bool HasToken(string userName, string tokenId)
        {
            return this._store.ContainsKey(userName) && this._store[userName].Any(t => t == tokenId);
        }

        public void Remove(string userName, string tokenId)
        {
            this._store[userName].Remove(tokenId);
        }
    }
}