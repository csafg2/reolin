var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            class RegisterInfo {
            }
            Client.RegisterInfo = RegisterInfo;
            class LoginInfo {
                get UserName() {
                    return this._userName;
                }
                set UserName(userName) {
                    this._userName = userName;
                }
                get Password() {
                    return this._password;
                }
                set Password(password) {
                    this._password = password;
                }
                IsValid() {
                    if (!this.UserName || !this.Password) {
                        return false;
                    }
                    return true;
                }
            }
            Client.LoginInfo = LoginInfo;
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
