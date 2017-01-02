
module Reolin.Web.Client
{
    export class RemoteJwtSource implements IJwtSource
    {
        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken
        {
            // site/exhange
            return null;
        }

        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken
        {
            //site// login
            return null;
        }
    }
}