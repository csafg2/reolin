﻿/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="../typing/jquery.d.ts" />

module Reolin.Web.Client
{
    export class HttpErrorEventArgs
    {
        private _retry      : boolean = false;
        

        get Retry(): boolean
        {
            return this._retry;
        }

        set(value: boolean)
        {
            this._retry = value;
        }
    }

    export class HttpService
    {
        protected headerCreating(headers: { [key: string]: string }): void
        {

        }

        public MakeRequest(httpMethod: string,
            url: string,
            requestData: any,
            headers: { [key: string]: string },
            retryCount: number,
            handler: HttpServiceHandler): void
        {
            this.headerCreating(headers);
            var me: HttpService = this;

            $.ajax({
                method: httpMethod,
                async: false,
                url: url,
                crossDomain: true,
                dataType: "json",
                data: requestData,
                beforeSend: function (xhr)
                {
                    for (var key in headers)
                    {
                        xhr.setRequestHeader(key, headers[key]);
                    }
                },
                success: function (responseData, textStatus, jqXHR)
                {
                    if (handler !== null && handler.HandleResponse !== null)
                    {
                        var response: HttpResponse = new HttpResponse();
                        response.ResponseBody = responseData;
                        response.StatusCode = jqXHR.status;
                        handler.HandleResponse(response);
                    }
                },
                error: function (xhr, error)
                {
                    if (handler !== null && handler.HandleError !== null)
                    {
                        var response: HttpResponse = new HttpResponse();
                        response.StatusCode = xhr.status;
                        response.ResponseBody = xhr.responseText;
                        handler.HandleError(response);
                    }

                    // allow sub class to play a role
                    var args: HttpErrorEventArgs = new HttpErrorEventArgs();
                    me.OnError(xhr, error, args);
                    if (args.Retry === true && retryCount > 0)
                    {
                        return me.MakeRequest(httpMethod, url, requestData, headers, retryCount--, handler);
                    }
                    else
                    {
                        throw error;
                    }
                }
            });
        };
        
        protected OnError(xhr: JQueryXHR, error: string, args: HttpErrorEventArgs): void
        {

        }

        public CreateFormData(input: any): FormData
        {
            var form_data = new FormData();

            for (var key in input)
            {
                form_data.append(key, input[key]);
            }

            return form_data;
        }
    }


    export class HttpServiceProvider extends HttpService
    {
        private _manager: IJwtManager;
        private _jwt: JwtSecurityToken;
        private _newTokenUrl: string;
        private _authenticaionFailed: AuthenticationFailedCallBack;

        //constructor();
        constructor(manager?: IJwtManager, authenticaionFailed?: AuthenticationFailedCallBack)
        {
            super();

            this._manager = manager;
            this._authenticaionFailed = authenticaionFailed;
        }

        protected headerCreating(headers: { [key: string]: string }): void
        {
            var jwt: JwtSecurityToken = this._manager.GetLocalJwt();

            if (jwt === null)
            {
                // user is not logged in!!
                this._authenticaionFailed();
                return;
            }
            else // user is logged in
            {
                headers["Authorization"] = "bearer " + jwt.Token;
            }
        }

        protected OnError(xhr: JQueryXHR, error: string, args: HttpErrorEventArgs)
        {
            // 401 states that token is expired basically
            if (xhr.status === 401 && this._manager.GetLocalJwt() !== null)
            {
                this._manager.ProvideJwtbyOldJwt(this._manager.GetLocalJwt());
                args.Retry = true;
            }
            // token is invalidated
            else if (xhr.status === 403)
            {
                this._authenticaionFailed();
                args.Retry = false;
            }
        }
    }

    export interface AuthenticationFailedCallBack
    {
        (): void;
    }

    export class HttpServiceHandler
    {
        public HandleResponse: (response: HttpResponse) => void;
        public HandleError: (response: HttpResponse) => void;
    }
}
