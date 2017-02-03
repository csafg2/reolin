
using System.Threading.Tasks;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtStore
    {
        /// <summary>
        /// attach specified tokenId to the uername
        /// </summary>
        /// <param name="userName">username that owns token id</param>
        /// <param name="tokenId">tokenId that will be attached</param>
        void Add(string userName, string tokenId);

        /// <summary>
        /// remove token from user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tokenId"></param>
        void Remove(string userName, string tokenId);

        /// <summary>
        /// determines if specified has a token with this Id
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        bool HasToken(string userName, string tokenId);

        Task AddAsync(string userName, string tokenId);
        Task<bool> HasTokenAsync(string userName, string tokenId);
        Task RemoveAsync(string userName, string tokenId);
    }
}
