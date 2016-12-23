using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reolin.Web.Security.Jwt;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using static Reolin.Web.Security.Jwt.JwtConstantsLookup;
using System.Text;

namespace JwtTests
{
    [TestClass]
    public class TestJwtManager
    {
        JwtManager manager = new JwtManager(new InMemoryJwtStore(), new JwtProvider());
        TokenProviderOptions options = new TokenProviderOptions()
        {
            Audience = JwtConfigs.Audience,
            Issuer = JwtConfigs.Issuer,
            SigningCredentials = JwtConfigs.SigningCredentials,
            Expiration = JwtConfigs.Expiry
        };

        string userName = "nina3";
        string[] roles = new[] { "admin", "superUser" };

        [TestMethod]
        public void TestTokenVerification()
        {
            options.Claims = GetPerUserClaims("nina3", 13, roles);
            var token = manager.IssueJwt(options);

            Assert.IsTrue(manager.VerifyToken(token, JwtConfigs.ValidationParameters));

            // simulate token forgery
            token = token.Replace("j", "C");
            Assert.IsFalse(manager.VerifyToken(token, JwtConfigs.ValidationParameters));
        }

        [TestMethod]
        public void TestExchange()
        {
            options.Claims = GetPerUserClaims("nina3", 13, roles);

            var firstToken = manager.IssueJwt(options);
            var jwt = new JwtSecurityToken(firstToken);

            bool firstTokenIsValid = manager.ValidateToken(userName, jwt.Id);
            Assert.IsTrue(firstTokenIsValid);
            
            var newToken = manager.ExchangeToken(new JwtSecurityToken(firstToken));
            Assert.IsNotNull(newToken);
        }


        private List<Claim> GetPerUserClaims(string userName, int userId, IEnumerable<string> roles)
        {
            return new List<Claim>()
                   {
                        new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.Integer64),
                        new Claim(Role_CLAIM_TYPE, GetRoleString(roles), ROLE_VALUE_TYPE),
                        new Claim(ID_CLAIM_TYPE, userId.ToString(), ClaimValueTypes.Integer32)
                   };
        }

        private string GetRoleString(IEnumerable<string> roles)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in roles)
            {
                sb.Append($"{item},");
            }

            return sb.ToString().Remove(sb.Length - 1);
        }
    }
}
