var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var JwtSecurityToken = (function () {
                function JwtSecurityToken(jwt) {
                    this._token = jwt;
                    this._tokenObject = this.Decode(jwt);
                }
                Object.defineProperty(JwtSecurityToken.prototype, "IsExpired", {
                    get: function () {
                        return false;
                    },
                    enumerable: true,
                    configurable: true
                });
                ;
                JwtSecurityToken.prototype.Decode = function (jwt) {
                    var base64Url = this._token.split('.')[1];
                    var base64 = base64Url.replace('-', '+').replace('_', '/');
                    return JSON.parse(window.atob(base64));
                };
                JwtSecurityToken.prototype.GetToken = function () {
                    return this._token;
                };
                JwtSecurityToken.Parse = function (jwt) {
                    if (!jwt) {
                        throw Error("jwt can not be null");
                    }
                    return new JwtSecurityToken(jwt);
                };
                ;
                JwtSecurityToken.TryParse = function (jwt) {
                    try {
                        return JwtSecurityToken.Parse(jwt);
                    }
                    catch (e) {
                        console.log(e.Message);
                        return null;
                    }
                };
                ;
                return JwtSecurityToken;
            }());
            Client.JwtSecurityToken = JwtSecurityToken;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
