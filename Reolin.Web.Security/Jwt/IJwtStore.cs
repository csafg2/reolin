using System.Collections.Generic;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtStore
    {
        void Add(string key, string tokenId);
        void Remove(string key, string tokenId);
        bool HasToken(string issuer, string tokenId);
    }
}
