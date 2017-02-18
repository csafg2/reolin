/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="../typing/jquery.d.ts" />
var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class HttpErrorEventArgs {
                constructor() {
                    this._retry = false;
                }
                get Retry() {
                    return this._retry;
                }
                set Retry(value) {
                    this._retry = value;
                }
            }
            Client.HttpErrorEventArgs = HttpErrorEventArgs;
            class HttpService {
                headerCreating(headers) {
                    if (headers === null)
                        headers = {};
                    console.log("in http service");
                }
                Get(url, headers, retryCount, isAsync = false, handler) {
                    this.MakeRequest("GET", url, null, headers, retryCount, handler, isAsync);
                }
                GetWithData(url, headers, retryCount, isAsync = false, data, handler) {
                    this.MakeRequest("GET", url, data, headers, retryCount, handler, isAsync);
                }
                Post(url, requestData, headers, retryCount, isAsync = false, handler, userOptions) {
                    this.MakeRequest("POST", url, requestData, headers, retryCount, handler, isAsync, userOptions);
                }
                MakeRequest(httpMethod, url, requestData, headers, retryCount, handler, isAsync = false, userOptions) {
                    this.headerCreating(headers);
                    var me = this;
                    console.log(isAsync);
                    var options = {
                        method: httpMethod,
                        async: isAsync,
                        url: url,
                        crossDomain: true,
                        data: requestData,
                        beforeSend: function (xhr) {
                            console.log(httpMethod);
                            for (var key in headers) {
                                console.log("adding " + key + " " + headers[key] + " in XHR header");
                                xhr.setRequestHeader(key, headers[key]);
                            }
                        },
                        success: function (responseData, textStatus, jqXHR) {
                            if (handler !== null && handler.HandleResponse !== undefined) {
                                var response = new Client.HttpResponse();
                                response.ResponseBody = responseData;
                                response.StatusCode = jqXHR.status;
                                response.ResponseText = jqXHR.responseText;
                                response.XHR = jqXHR;
                                handler.HandleResponse(response);
                            }
                        },
                        error: function (xhr, status, error) {
                            if (handler !== null && handler.HandleError !== undefined) {
                                var response = new Client.HttpResponse();
                                response.StatusCode = xhr.status;
                                response.ResponseBody = xhr.responseText;
                                response.Error = error;
                                handler.HandleError(response);
                            }
                            // allow sub class to play a role
                            var args = new HttpErrorEventArgs();
                            me.OnError(xhr, error, args);
                            if (args.Retry === true && retryCount > 0) {
                                return me.MakeRequest(httpMethod, url, requestData, headers, --retryCount, handler);
                            }
                        }
                    };
                    for (var p in userOptions) {
                        options[p] = userOptions[p];
                        console.log(options[p]);
                    }
                    $.ajax(options);
                }
                ;
                OnError(xhr, error, args) {
                }
                CreateFormData(input) {
                    var formData = new FormData();
                    for (var key in input) {
                        formData.append(key, input[key]);
                    }
                    return formData;
                }
            }
            Client.HttpService = HttpService;
            class AuthenticatedHttpServiceProvider extends HttpService {
                constructor(manager, authenticaionFailed) {
                    super();
                    this._authenticationScheme = "bearer ";
                    this._headerKey = "Authorization";
                    this._manager = manager;
                    this._authenticaionFailed = authenticaionFailed;
                }
                headerCreating(headers) {
                    if (headers === null) {
                        return;
                    }
                    var jwt = this._manager.GetLocalJwt();
                    if (jwt === null) {
                        // the fucking user is not logged in!! :D
                        // force his ass to move to login page.
                        this._authenticaionFailed();
                        return;
                    }
                    else {
                        headers[this._headerKey] = this._authenticationScheme + jwt.Token;
                    }
                }
                OnError(xhr, error, args) {
                    // 401 states that token is expired basically
                    if (xhr.status === 401 && this._manager.GetLocalJwt() !== null) {
                        this._manager.ProvideJwtbyOldJwt(this._manager.GetLocalJwt());
                        args.Retry = true;
                    }
                    else if (xhr.status === 403) {
                        args.Retry = false;
                        this._authenticaionFailed();
                    }
                }
            }
            Client.AuthenticatedHttpServiceProvider = AuthenticatedHttpServiceProvider;
            class HttpServiceHandler {
            }
            Client.HttpServiceHandler = HttpServiceHandler;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
