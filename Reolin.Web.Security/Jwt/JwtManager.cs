using System;
using Newtonsoft.Json;

namespace Reolin.Web.Security.Jwt
{
    public static class JwtManager
    {

        public static string CreateResponseString(string jwt, TimeSpan expiry)
        {
            return JsonConvert.SerializeObject(
                new
                {
                    access_token = jwt,
                    expires_in = expiry
                });
        }
    }
}