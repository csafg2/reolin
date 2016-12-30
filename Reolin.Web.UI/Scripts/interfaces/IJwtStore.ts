/// <reference path="../jwtsecuritytoken.ts" />

module Reolin.Web.Client {
    class sample {
        getValue(): LoginInfo {
            return null;
        }
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

    export class LocalStorageJwtStore implements IJwtStore {
        private key: string = "jwt";

        HasJwt(): boolean {
            return (window.localStorage.getItem(this.key) != null);
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
    
    export class DefaultJwtProvider implements IJwtManager {

        private _store: IJwtStore;
        private _source: IJwtSource;

        get Store(): IJwtStore {
            return this._store;
        }

        set Store(store: IJwtStore) {
            this._store = store;
        }

        get Source(): IJwtSource {
            return this._source;
        }

        set Source(source: IJwtSource) {
            this._source = source;
        }

        ProvideJwt: (loginInfo: LoginInfo) => JwtSecurityToken = function (loginInfo: LoginInfo) {
            var jwt: JwtSecurityToken;

            if (loginInfo == null && this.Store.hasJwt()) {
                jwt = this.Store.Get();
                if (jwt.IsExpired) {
                    try {
                        jwt = this.Source.ExchangeJwt(jwt);
                        this.Store.Save(jwt);
                    }
                    catch (e) {
                        console.log(e.message);
                    }
                }
            }
            else {
                try {
                    jwt = this.Source.IssueJwt(loginInfo);
                    this.Store.Save(jwt);
                }
                catch (e) {
                    console.log(e.message);
                }
            }

            return jwt;
        }
    }
}

module Reolin.Web.Client.Core {
    export interface IJwtStore {
        Get(): JwtSecurityToken;
        Save(jwt: JwtSecurityToken): void;
        HasJwt(): boolean;
    }

    export interface IJwtSource {
        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken;
    }
    
    export interface IJwtManager {
        Store: IJwtStore;
        Source: IJwtSource;
        ProvideJwt(loginInfo: LoginInfo): JwtSecurityToken;
    }
}
