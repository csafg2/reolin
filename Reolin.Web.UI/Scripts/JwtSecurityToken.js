var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var JwtSecurityToken = (function () {
                function JwtSecurityToken() {
                    this._isExpired = false;
                    this.GetToken = function () {
                        return this._token;
                    };
                }
                Object.defineProperty(JwtSecurityToken.prototype, "IsExpired", {
                    get: function () {
                        return this._isExpired;
                    },
                    set: function (value) {
                        this._isExpired = value;
                    },
                    enumerable: true,
                    configurable: true
                });
                ;
                ;
                JwtSecurityToken.Parse = function (jwt) {
                    return new JwtSecurityToken();
                };
                JwtSecurityToken.TryParse = function (jwt) {
                    return null;
                };
                return JwtSecurityToken;
            }());
            Client.JwtSecurityToken = JwtSecurityToken;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
