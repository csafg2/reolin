/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="../typing/jquery.d.ts" />

module Reolin.Web.Client
{
    export class HttpService
    {
        protected GetHeaders(): { [key: string]: string }
        {
            return null;
        }

        Get(url: string): HttpResponse
        {
            var headers = this.GetHeaders();
            
            $.ajax({
                type: "GET",
                beforeSend: function (xhr)
                {
                    for (var key in headers)
                    {
                        xhr.setRequestHeader(key, headers[key]);
                    }
                },
                success: function (data)
                {
                    // var result: /*HttpResponse = new HttpResponse(); */
                },
                error: function (xhr, error)
                {
                    this.OnError(xhr, error);
                }
            });

            return null;
        };


        protected OnError(xhr: JQueryXHR, error: string): void
        {

        }
    }


    class HttpServiceProvider extends HttpService
    {
        private _manager: IJwtManager;
        private _jwt: JwtSecurityToken;
        private _newTokenUrl: string;
        private _authenticaionFailed: AuthenticationFailedCallBack;

        constructor();
        constructor(manager?: IJwtManager, authenticaionFailed?: AuthenticationFailedCallBack)
        {
            super();

            this._manager = manager;
            this._authenticaionFailed = authenticaionFailed;
        }

        protected GetHeaders(): any
        {
            var result: { [key: string]: string };
            var jwt: JwtSecurityToken = this._manager.GetLocalJwt();

            if (jwt === null)
            {
                // user is not logged in!!
                this._authenticaionFailed();
                return null;
            }
            else // user is logged in
            {
                result["Authorization"] = "bearer " + jwt.Token;
            }

            return result;
        }

        protected OnError(xhr: JQueryXHR, error: string)
        {
            // inspect the error code, if token is invalidate then force login
            // if token is expired the exchange it and then retry.
            
            if (xhr.status === 401 && this._manager.GetLocalJwt() !== null)
            {

            }
            alert(xhr.status);
        }
    }

    export interface AuthenticationFailedCallBack
    {
        (): void;
    }
}
