/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />

module Reolin.Web.UI {
    class HttpService {

        private _jwt: JwtSecurityToken;
        private _newTokenUrl: string;

        constructor();
        constructor(jwt?: JwtSecurityToken, newTokenUrl?: string) {
            this._jwt = jwt;
            this._newTokenUrl = newTokenUrl;
        }

        Get: (url: string, headers: { [key: string]: string }) =>
            HttpResponse = function (url: string, headers: { [key: string]: string }) {
                headers["formData"] = "123Hellow world!";
                return null;
            };
    }
}
