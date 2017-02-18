/// <reference path="../../typing/jquery.d.ts" />
var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                class AccountController {
                    constructor(userLoggedInCallBack) {
                        this.LoginButton = $('#LoginButton');
                        this.RegisterButton = $('#RegisterButton');
                        this.PasswordTextBox = $('#Password');
                        this.ConfirmPasswordTextBox = $('#ConfirmPassword');
                        this.EmailTextBox = $('#Email');
                        this.UserNameTextBox = $('#UserName');
                        this.ErrorList = $("#ErrorList");
                        this._service = new Reo.AccountService(manager);
                        this.SetGlobalHandlers();
                        this._userLoggedInCallBack = userLoggedInCallBack;
                        this.LoginButton.click(e => this.LoginButton_ClickHandler(e));
                        this.RegisterButton.click(e => this.RegisterButton_ClickHandler(e));
                    }
                    SetGlobalHandlers() {
                        var me = this;
                        $(document).ajaxError(function (event, jqxhr, settings, thrownError) {
                            if (!(settings.url.toLowerCase().indexOf("/account/login") > 0)) {
                                return;
                            }
                            me.ErrorList.html("");
                            for (var key in jqxhr.responseJSON) {
                                me.ErrorList.append('<p style="color:red">' + jqxhr.responseJSON[key] + '</p>');
                            }
                        });
                        $(document).ajaxSuccess(function (event, xhr, settings) {
                            if (!(settings.url.toLowerCase().indexOf("/account/login") > 0)) {
                                return;
                            }
                            //me._userLoggedInCallBack();
                            //window.location.href = "/";
                        });
                    }
                    RegisterButton_ClickHandler(e) {
                        e.preventDefault();
                        var info = new Client.RegisterInfo();
                        info.UserName = this.UserNameTextBox.val();
                        info.Password = this.PasswordTextBox.val();
                        info.ConfirmPassword = this.ConfirmPasswordTextBox.val();
                        info.Email = this.EmailTextBox.val();
                        var handler = new Client.HttpServiceHandler();
                        handler.HandleResponse = (r) => {
                            console.log("User has been registered");
                            var loginInfo = new Client.LoginInfo();
                            loginInfo.UserName = info.UserName;
                            loginInfo.Password = info.Password;
                            console.log("initiating login");
                            this._service.Login(loginInfo);
                            this._userLoggedInCallBack();
                        };
                        this._service.Register(info, handler);
                    }
                    LoginButton_ClickHandler(e) {
                        var info = new Client.LoginInfo();
                        info.UserName = this.UserNameTextBox.val();
                        info.Password = this.PasswordTextBox.val();
                        this._service.Login(info);
                        this._userLoggedInCallBack();
                    }
                }
                Controllers.AccountController = AccountController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
