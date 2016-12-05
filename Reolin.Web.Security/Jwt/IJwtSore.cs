using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace Reolin.Web.Security.Jwt
{
    public interface IJwtSore
    {
        void Add(string key, string token);
        void Remove(string key, string jwt);
        bool TryRemove(string key, string token);
        List<string> Get(string key);
    }
    class MyClass
    {
        public MyClass()
        {
            
        }
    }
}
