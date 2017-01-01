/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
/// <reference path="typing/jquery.d.ts" />
var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var HttpService = (function () {
                function HttpService(jwt, newTokenUrl) {
                    this._jwt = jwt;
                    this._newTokenUrl = newTokenUrl;
                }
                HttpService.prototype.Get = function (url, header) {
                    $.ajax({
                        type: "GET",
                        headers: header,
                        beforeSend: function () {
                        },
                        success: function (data) {
                            var result; /*HttpResponse = new HttpResponse(); */
                        }
                    });
                    return null;
                };
                ;
                return HttpService;
            }());
            Client.HttpService = HttpService;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
