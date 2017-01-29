/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="../typing/jquery.d.ts" />

module Reolin.Web.Client {
    export class HttpErrorEventArgs {
        private _retry: boolean = false;


        get Retry(): boolean {
            return this._retry;
        }

        set Retry(value: boolean) {
            this._retry = value;
        }
    }

    export class HttpService {
        protected headerCreating(headers: { [key: string]: string }): void {
            if (headers === null) headers = {};

            console.log("in http service");
        }

        public Get(url: string,
            headers: { [key: string]: string },
            retryCount: number,
            isAsync: boolean = false,
            handler: HttpServiceHandler): void {

            this.MakeRequest("GET", url, null, headers, retryCount, handler, isAsync);

        }

        public Post(url: string,
            requestData: any,
            headers: { [key: string]: string },
            retryCount: number,
            isAsync: boolean = false,
            handler: HttpServiceHandler): void {

            this.MakeRequest("POST", url, requestData, headers, retryCount, handler, isAsync);
        }

        public MakeRequest(httpMethod: string,
            url: string,
            requestData: any,
            headers: { [key: string]: string },
            retryCount: number,
            handler: HttpServiceHandler, isAsync: boolean = false): void {

            this.headerCreating(headers);

            var me: HttpService = this;
            console.log(isAsync);
            $.ajax({
                method: httpMethod,
                async: isAsync,
                url: url,
                crossDomain: true,
                //dataType: "json",
                data: requestData,
                beforeSend: function (xhr) {
                    console.log(httpMethod);
                    for (var key in headers) {
                        console.log("adding " + key + " " + headers[key] + " in XHR header");
                        xhr.setRequestHeader(key, headers[key]);
                    }
                },
                success: function (responseData, textStatus: string, jqXHR: JQueryXHR) {
                    if (handler !== null && handler.HandleResponse !== undefined) {
                        var response: HttpResponse = new HttpResponse();
                        response.ResponseBody = responseData;
                        response.StatusCode = jqXHR.status;
                        response.ResponseText = jqXHR.responseText;
                        handler.HandleResponse(response);
                    }
                },
                error: function (xhr, status, error) {
                    if (handler !== null && handler.HandleError !== undefined) {
                        var response: HttpResponse = new HttpResponse();
                        response.StatusCode = xhr.status;
                        response.ResponseBody = xhr.responseText;
                        response.Error = error;
                        handler.HandleError(response);
                    }

                    // allow sub class to play a role
                    var args: HttpErrorEventArgs = new HttpErrorEventArgs();
                    me.OnError(xhr, error, args);
                    if (args.Retry === true && retryCount > 0) {
                        return me.MakeRequest(httpMethod, url, requestData, headers, --retryCount, handler);
                    }
                }
            });
        };

        protected OnError(xhr: JQueryXHR, error: string, args: HttpErrorEventArgs): void {

        }

        public CreateFormData(input: any): FormData {
            var formData = new FormData();

            for (var key in input) {
                formData.append(key, input[key]);
            }

            return formData;
        }
    }

    export class AuthenticatedHttpServiceProvider extends HttpService {
        private _manager: IJwtManager;
        private _jwt: JwtSecurityToken;
        private _newTokenUrl: string;
        private _authenticaionFailed: AuthenticationFailedCallBack;

        private _authenticationScheme: string = "bearer ";
        private _headerKey: string = "Authorization";

        constructor(manager?: IJwtManager, authenticaionFailed?: AuthenticationFailedCallBack) {
            super();

            this._manager = manager;
            this._authenticaionFailed = authenticaionFailed;
        }

        protected headerCreating(headers: { [key: string]: string }): void {
            if (headers === null) {
                return;
            }

            var jwt: JwtSecurityToken = this._manager.GetLocalJwt();

            if (jwt === null) {
                // the fucking user is not logged in!! :D
                // force his ass to move to login page.
                this._authenticaionFailed();
                return;
            }
            else // user is logged in
            {
                headers[this._headerKey] = this._authenticationScheme + jwt.Token;
            }
        }

        protected OnError(xhr: JQueryXHR, error: string, args: HttpErrorEventArgs) {
            // 401 states that token is expired basically
            if (xhr.status === 401 && this._manager.GetLocalJwt() !== null) {
                this._manager.ProvideJwtbyOldJwt(this._manager.GetLocalJwt());
                args.Retry = true;
            }

            // this FUCKING user some how invalidated token
            else if (xhr.status === 403) {
                args.Retry = false;
                this._authenticaionFailed();
            }
        }
    }

    export interface AuthenticationFailedCallBack {
        (): void;
    }

    export class HttpServiceHandler {
        public HandleResponse: (response: HttpResponse) => void;
        public HandleError: (response: HttpResponse) => void;
    }
}
