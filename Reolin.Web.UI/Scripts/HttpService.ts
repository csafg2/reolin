/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="typing/jquery.d.ts" />

module Reolin.Web.Client {
    export class HttpService {

        private _jwt: JwtSecurityToken;
        private _newTokenUrl: string;

        constructor();
        constructor(jwt?: JwtSecurityToken, newTokenUrl?: string) {
            this._jwt = jwt;
            this._newTokenUrl = newTokenUrl;
        }

        Get(url: string, header: { [key: string]: string }): HttpResponse {
            $.ajax({
                type : "GET",
                headers : header
            });
            
            return null;
        };
    }
}
