/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="../typing/jquery.d.ts" />

module Reolin.Web.Client {
    
    export interface IJwtStore {
        HasJwt(): boolean;
        Get(): JwtSecurityToken;
        Save(jwt: JwtSecurityToken): void;
    }
}
