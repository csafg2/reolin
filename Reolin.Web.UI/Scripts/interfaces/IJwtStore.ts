/// <reference path="../jwtsecuritytoken.ts" />
/// <reference path="../typing/jquery.d.ts" />

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
        HasJwt(): boolean;
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
        private _source: IJwtSource;
        private _store: IJwtStore;

        constructor(source: IJwtSource, store: IJwtStore) {
            this._source = source;
            this._store = store;
        }

        GetLocalJwt(): JwtSecurityToken {
            if (!this._store.HasJwt()) {
                return null;
            }
            return this._store.Get();
        }

        ProvideJwtByLoginInfo(info: LoginInfo): JwtSecurityToken {
            if (info === null) {
                throw new Error("info can not be null");
            }

            return this._source.IssueJwt(info);
        }

        ProvideJwtbyOldJwt(oldJwt: JwtSecurityToken): JwtSecurityToken {
            if (oldJwt === null) {
                throw new Error("oldJwt can not be null");
            }

            return this._source.ExchangeJwt(oldJwt);
        }
    }

    export class LocalJwtStore implements IJwtStore {
        private key: string = "jwt";

        HasJwt(): boolean {
            return (window.localStorage.getItem(this.key) !== null);
        }

        Get(): JwtSecurityToken {
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
        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken {
            
            return null;
        }

        IssueJwt: (loginInfo: LoginInfo) => JwtSecurityToken = function (loginInfo: LoginInfo) {
            return null;
        }
    }
}
