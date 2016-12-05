using System.Collections.Generic;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtStore
    {
        void Add(string key, string token);
        void Remove(string key, string jwt);
        bool TryRemove(string key, string token);
        List<string> Get(string key);
        bool HasToken(string issuer, string token);
    }
}
