var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var UI;
        (function (UI) {
            var JwtSecurityToken = (function () {
                function JwtSecurityToken() {
                    this._isExpired = false;
                    this.Parse = function (jwt) {
                        return new JwtSecurityToken();
                    };
                    this.TryParse = function (jwt) {
                        return null;
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
                return JwtSecurityToken;
            }());
            UI.JwtSecurityToken = JwtSecurityToken;
        })(UI = Web.UI || (Web.UI = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
