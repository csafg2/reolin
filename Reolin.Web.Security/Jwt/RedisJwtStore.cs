using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Security.Jwt
{
    public class RedisJwtStore : IJwtStore
    {
        private readonly IDatabase _dataBase;

        public RedisJwtStore(IConnectionMultiplexer mutex)
        {
            this._dataBase = mutex.GetDatabase();
        }


        private IDatabase RedisDatabase
        {
            get
            {
                return _dataBase;
            }
        }


        public void Add(string userName, string tokenId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentException("Key or tokenId Can not be null.");
            }

            this.RedisDatabase.ListLeftPush(userName, tokenId);
        }

        public async Task AddAsync(string userName, string tokenId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentException("Key or tokenId Can not be null.");
            }

            await this.RedisDatabase.ListLeftPushAsync(userName, tokenId);
        }

        public bool HasToken(string userName, string tokenId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentNullException($"{nameof(userName)} and {nameof(tokenId)} are required.");
            }

            return this.RedisDatabase.ListRange(userName).Any(t => t == tokenId);
        }

        public async Task<bool> HasTokenAsync(string userName, string tokenId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentNullException($"{nameof(userName)} and {nameof(tokenId)} are required.");
            }

            return (await this.RedisDatabase.ListRangeAsync(userName)).Any(t => t == tokenId);
        }

        public void Remove(string userName, string tokenId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentNullException($"{nameof(userName)} and {nameof(tokenId)} are both required");
            }

            this.RedisDatabase.ListRemove(userName, tokenId);
        }

        public async Task RemoveAsync(string userName, string tokenId)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(tokenId))
            {
                throw new ArgumentNullException($"{nameof(userName)} and {nameof(tokenId)} are both required");
            }

            await this.RedisDatabase.ListRemoveAsync(userName, tokenId);
        }
    }
}
