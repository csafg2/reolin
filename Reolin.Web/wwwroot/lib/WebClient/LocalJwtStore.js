var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class LocalJwtStore {
                constructor() {
                    this.key = "jwt";
                }
                HasJwt() {
                    return (window.localStorage.getItem(this.key) !== null);
                }
                Get() {
                    return Client.JwtSecurityToken.Parse(window.localStorage.getItem(this.key));
                }
                Save(jwt) {
                    console.log("saveing" + jwt.Token);
                    if (jwt === null) {
                        throw Error("jwt can not be null");
                    }
                    window.localStorage.clear();
                    window.localStorage.setItem(this.key, jwt.Token);
                }
            }
            Client.LocalJwtStore = LocalJwtStore;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
