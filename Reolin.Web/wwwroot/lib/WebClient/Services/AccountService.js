var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class AccountService {
                constructor(jwtManager) {
                    this._jwtManager = jwtManager;
                }
                Register(registerInfo, handler) {
                    // send off a request to register this user
                    var service = new Client.HttpService();
                    service.Post(UrlSource.RegisterAccount, registerInfo, null, 2, true, handler);
                }
                Login(info) {
                    var token = manager.IssueJwt(info);
                }
                Relogin() {
                    var jwt = this._jwtManager.GetLocalJwt();
                    if (jwt === null) {
                        throw new Error("oldJwt can not be null");
                    }
                    this._jwtManager.ProvideJwtbyOldJwt(jwt);
                }
            }
            Client.AccountService = AccountService;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
