/// <reference path="httpresponse.ts" />
/// <reference path="jwtsecuritytoken.ts" />
var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var UI;
        (function (UI) {
            var HttpService = (function () {
                function HttpService(jwt, newTokenUrl) {
                    this.Get = function (url, headers) {
                        headers["formData"] = "123Hellow world!";
                        return null;
                    };
                    this._jwt = jwt;
                    this._newTokenUrl = newTokenUrl;
                }
                return HttpService;
            }());
        })(UI = Web.UI || (Web.UI = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
