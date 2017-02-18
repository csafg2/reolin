var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class RemoteJwtSource {
                constructor(exhangeUrl, getJwtUrl) {
                    if (IsNullOrEmpty(exhangeUrl, getJwtUrl)) {
                        throw new Error("exhangeUrl and getJwtUrl can not be null");
                    }
                    this._exhangeUrl = exhangeUrl;
                    this._getJwtUrl = getJwtUrl;
                }
                ExchangeJwt(oldJwt) {
                    if (oldJwt === null) {
                        throw new Error("OldJwt Can not be null");
                    }
                    var service = new Client.HttpService();
                    var headers = {};
                    headers["Authorization"] = "bearer " + oldJwt.Token;
                    var handler = new Client.HttpServiceHandler();
                    var result = null;
                    handler.HandleResponse = (response) => {
                        result = Client.JwtSecurityToken.Parse(response.ResponseBody.newToken);
                    };
                    service.MakeRequest("POST", this._exhangeUrl, null, headers, 2, handler, false);
                    return result;
                }
                IssueJwt(loginInfo) {
                    if (loginInfo === null) {
                        throw new Error("loginInfo can not be null");
                    }
                    var service = new Client.HttpService();
                    var requestData = { UserName: loginInfo.UserName, Password: loginInfo.Password };
                    var handler = new Client.HttpServiceHandler();
                    var result = null;
                    handler.HandleResponse = (response) => {
                        result = Client.JwtSecurityToken.Parse(response.ResponseBody.accessToken);
                    };
                    service.MakeRequest("POST", this._getJwtUrl, requestData, null, 3, handler, false);
                    return result;
                }
            }
            Client.RemoteJwtSource = RemoteJwtSource;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
