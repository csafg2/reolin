var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class JwtSecurityToken {
                constructor(token) {
                    this._tokenValues = {};
                    this._token = token;
                }
                get IsExpired() {
                    // we do`nt fucking trust client machine`s time
                    return false;
                }
                ;
                static Decode(jwt) {
                    var base64Url = jwt.split('.')[1];
                    var base64 = base64Url.replace('-', '+').replace('_', '/');
                    return JSON.parse(window.atob(base64));
                }
                get Token() {
                    return this._token;
                }
                GetKey(key) {
                    return this._tokenValues[key];
                }
                static Parse(jwt) {
                    if (IsNullOrEmpty(jwt)) {
                        throw Error("jwt can not be null");
                    }
                    var result = new JwtSecurityToken(jwt);
                    var jst = JwtSecurityToken.Decode(jwt);
                    for (var p in jst) {
                        result._tokenValues[p] = jst[p];
                    }
                    return result;
                }
                ;
                static TryParse(jwt) {
                    if (IsNullOrEmpty(jwt)) {
                        throw new Error("Jwt can not be null");
                    }
                    try {
                        return JwtSecurityToken.Parse(jwt);
                    }
                    catch (e) {
                        if (console) {
                            console.log(e.Message);
                        }
                        return null;
                    }
                }
                ;
            }
            Client.JwtSecurityToken = JwtSecurityToken;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
