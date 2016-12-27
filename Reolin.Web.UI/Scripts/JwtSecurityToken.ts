module Reolin.Web.UI {
    export class JwtSecurityToken {
        private _isExpired: boolean = false;
        private _token: string;

        get IsExpired(): boolean {
            return this._isExpired;
        };

        set IsExpired(value: boolean) {
            this._isExpired = value;
        };

        GetToken: () => string = function () {
            return this._token;
        };

        static Parse: (jwt: string) => JwtSecurityToken = function (jwt: string) {
            return new JwtSecurityToken();
        };

        static TryParse: (jwt: string) => JwtSecurityToken = function (jwt: string) {
            return null;
        };
    }
}