
module Reolin.Web.Client
{

    export interface IJwtManager
    {
        GetLocalJwt(): JwtSecurityToken;
        IssueJwt(info: LoginInfo): JwtSecurityToken;
        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
        Save(jwt: JwtSecurityToken);
    }
}