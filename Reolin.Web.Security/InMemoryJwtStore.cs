using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentNullException("Key or tokenId Can not be null");
            }
            _store[key] = new List<string>() { tokenId };
        }

        public Task AddAsync(string userName, string tokenId)
        {
            this.Add(userName, tokenId);
            return Task.FromResult(0);
        }

        public bool HasToken(string userName, string tokenId)
        {
            return this._store.ContainsKey(userName) && this._store[userName].Any(t => t == tokenId);
        }

        public Task<bool> HasTokenAsync(string userName, string tokenId)
        {
            return Task.FromResult(this.HasToken(userName, tokenId));
        }

        public void Remove(string userName, string tokenId)
        {
            this._store[userName].Remove(tokenId);
        }

        public Task RemoveAsync(string userName, string tokenId)
        {
            this.Remove(userName, tokenId);
            return Task.FromResult(0);
        }
    }
}