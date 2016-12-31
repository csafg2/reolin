/// <reference path="../jwtsecuritytoken.ts" />

module Reolin.Web.Client {
    export interface IJwtSource {
        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken;
    }

    export interface IJwtProvider {
        GetLocalJwt(): JwtSecurityToken;
        ProvideJwtByLoginInfo(info: LoginInfo): JwtSecurityToken;
        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
    }

    export interface IJwtStore {
        Get(): JwtSecurityToken;
        Save(jwt: JwtSecurityToken): void;
    }

    export class LoginInfo {
        private _userName: string;
        private _password: string;

        get UserName(): string {
            return this._userName;
        }

        set UserName(userName: string) {
            this._userName = userName;
        }

        get Password(): string {
            return this._password;
        }

        set Password(password: string) {
            this._password = password;
        }
    }

    export class DefaultJwtProvider implements IJwtProvider {
        GetLocalJwt(): JwtSecurityToken {
            return null;
        }

        ProvideJwtByLoginInfo(info: LoginInfo): JwtSecurityToken {

            return null;
        }

        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken {
            return null;
        }
    }

    export class LocalJwtStore implements IJwtStore {
        private key: string = "jwt";

        HasJwt(): boolean {
            return (window.localStorage.getItem(this.key) !== null);
        }

        Get(): JwtSecurityToken {
            if (!this.HasJwt()) {
                throw new Error("jwt dose not exist");
            }

            return JwtSecurityToken.Parse(window.localStorage.getItem(this.key));
        }


        Save(jwt: JwtSecurityToken): void {
            if (jwt == null) {
                throw Error("jwt can not be null");
            }

            window.localStorage.clear();
            window.localStorage.setItem(this.key, jwt.GetToken());
        }
    }

    export class RemoteJwtSource implements IJwtSource {
        ExchangeJwt: (oldJwt: JwtSecurityToken) => JwtSecurityToken = function (oldJwt: JwtSecurityToken) {
            return null;
        }

        IssueJwt: (loginInfo: LoginInfo) => JwtSecurityToken = function (loginInfo: LoginInfo) {
            return null;
        }
    }
}
