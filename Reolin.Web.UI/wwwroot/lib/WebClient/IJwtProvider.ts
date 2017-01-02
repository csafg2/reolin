
module Reolin.Web.Client
{

    export interface IJwtProvider
    {
        GetLocalJwt(): JwtSecurityToken;
        ProvideJwtByLoginInfo(info: LoginInfo): JwtSecurityToken;
        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
    }
}