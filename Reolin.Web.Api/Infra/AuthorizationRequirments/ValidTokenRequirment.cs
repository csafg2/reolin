using Microsoft.AspNetCore.Authorization;
using Reolin.Web.Security.Jwt;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.AuthorizationRequirments
{

    public class ValidTokenRequirment : AuthorizationHandler<ValidTokenRequirment>, IAuthorizationRequirement
    {
        private IJwtManager _jwtManager;

        public ValidTokenRequirment(IJwtManager manager, IServiceProvider provider)
        {
            _jwtManager = manager;
        }

        public static string Name { get { return "ValidJwt"; } }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidTokenRequirment requirement)
        {
            string tokenId = context.User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
            string userName = context.User.Claims.GetUsernameClaim()?.Value;

            if (string.IsNullOrEmpty(tokenId) || string.IsNullOrEmpty(userName))
            {
                context.Fail();
                return Task.FromResult(0);
            }

            if (!_jwtManager.ValidateToken(userName, tokenId))
            {
                context.Fail();
            }
            else
            {
                context.Succeed(this);
            }

            return Task.FromResult(0);
        }
    }
}
