var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class HttpResponse {
                get XHR() {
                    return this._xhr;
                }
                set XHR(value) {
                    this._xhr = value;
                }
                get Error() {
                    return this._error;
                }
                set Error(error) {
                    this._error = error;
                }
                get StatusCode() {
                    return this._statusCode;
                }
                set StatusCode(code) {
                    this._statusCode = code;
                }
                get ResponseHeaders() {
                    return this._responseHeaders;
                }
                set ResponseHeaders(headers) {
                    this._responseHeaders = headers;
                }
                get ResponseBody() {
                    return this._responseBody;
                }
                set ResponseBody(body) {
                    this._responseBody = body;
                }
                get ResponseText() {
                    return this._text;
                }
                set ResponseText(value) {
                    this._text = value;
                }
            }
            Client.HttpResponse = HttpResponse;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
