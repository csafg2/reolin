/// <reference path="../jwtsecuritytoken.ts" />

module Reolin.Web.UI.Scripts.interfaces {

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
    
    export interface IJwtStore {
        Get(): JwtSecurityToken;
        Save(jwt: JwtSecurityToken): void;
        HasJwt(): boolean;
    }

    export class LocalStorageJwtStore implements IJwtStore {
        private key: string = "jwt";

        HasJwt: () => boolean = function () {
            return (window.localStorage.getItem(this.key) != null);
        }

        Get: () => JwtSecurityToken = function () {
            if (!this.HasJwt()) {
                throw new Error("jwt dose not exist");
            }

            return JwtSecurityToken.Parse(window.localStorage.getItem(this.key));
        }

        Save: (jwt: JwtSecurityToken) => void = function (jwt: JwtSecurityToken) {
            if (jwt == null) {
                throw Error("jwt can not be null");
            }

            window.localStorage.clear();
            window.localStorage.setItem(this.key, jwt.GetToken());
        }

    }

    export interface IJwtSource {
        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken;
        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken;
    }


    export class RemoteJwtSource implements IJwtSource {
        ExchangeJwt: (oldJwt: JwtSecurityToken) => JwtSecurityToken = function (oldJwt: JwtSecurityToken) {
            return null;
        }

        IssueJwt: (loginInfo: LoginInfo) => JwtSecurityToken = function (loginInfo: LoginInfo) {
            return null;
        }
    }

    export interface IJwtProvider {
        Store: IJwtStore;
        Source: IJwtSource;
        ProvideJwt(loginInfo: LoginInfo): JwtSecurityToken;
    }

    export class DefaultJwtProvider implements IJwtProvider {

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
            var jwt: JwtSecurityToken = this.Store.HasJwt();
            if (!jwt.IsExpired) {
                return jwt;
            }
            else {
                return null;
            }
        }
    }
}
