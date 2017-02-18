var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class DefaultJwtManager {
                constructor(source, store) {
                    this._source = source;
                    this._store = store;
                }
                GetLocalJwt() {
                    if (!this._store.HasJwt()) {
                        return null;
                    }
                    return this._store.Get();
                }
                Save(jwt) {
                    this._store.Save(jwt);
                }
                IssueJwt(info) {
                    var jwt = this._source.IssueJwt(info);
                    if (jwt === null) {
                        return null;
                    }
                    this.Save(jwt);
                    return jwt;
                }
                ProvideJwtbyOldJwt(oldJwt) {
                    if (oldJwt === null) {
                        throw new Error("oldJwt can not be null");
                    }
                    var newToken = this._source.ExchangeJwt(oldJwt);
                    this._store.Save(newToken);
                    return newToken;
                }
            }
            Client.DefaultJwtManager = DefaultJwtManager;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
