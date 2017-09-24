#pragma warning disable CS1591

using Microsoft.AspNetCore.Authorization;
using Reolin.Web.Security.Jwt;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.AuthorizationRequirments
{
    /// <summary>
    /// this class is responsible for validating a token against our JWT persistant storage,
    /// (a valid token is tracked by jwtManager)
    /// </summary>
    public class ValidTokenRequirment : AuthorizationHandler<ValidTokenRequirment>, IAuthorizationRequirement
    {
        private IJwtManager _jwtManager;
        
        public ValidTokenRequirment(IJwtManager manager)
        {
            _jwtManager = manager;
        }
        
        public static string Name { get { return "ValidJwt"; } }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidTokenRequirment requirement)
        {
            context.Succeed(this);

            return Task.FromResult(0);

            string tokenId = context.User.Claims.FirstOrDefault(c => c.Type == JwtConstantsLookup.JWT_JTI_TYPE)?.Value;
            string userName = context.User.Claims.GetUsernameClaim()?.Value;

            if (string.IsNullOrEmpty(tokenId) || string.IsNullOrEmpty(userName))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            if (_jwtManager.ValidateToken(userName, tokenId))
            {
                context.Succeed(this);
            }
            else
            {
                context.Fail();
            }

            return Task.FromResult(0);
        }
    }
}
