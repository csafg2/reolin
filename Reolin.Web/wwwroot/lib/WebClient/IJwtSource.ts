module Reolin.Web.Client
{
    export interface IJwtSource
    {
        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken;
    }
}