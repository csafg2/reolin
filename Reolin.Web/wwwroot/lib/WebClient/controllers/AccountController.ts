﻿/// <reference path="../../typing/jquery.d.ts" />

module Reolin.Web.Client.Controllers
{

    export class AccountController
    {
        LoginButton: JQuery = $('#LoginButton');
        RegisterButton: JQuery = $('#RegisterButton');

        PasswordTextBox: JQuery = $('#Password');
        ConfirmPasswordTextBox: JQuery = $('#ConfirmPassword');
        EmailTextBox: JQuery = $('#Email');
        UserNameTextBox: JQuery = $('#UserName');

        ErrorList: JQuery = $("#ErrorList");

        private _service: Reo.AccountService = new Reo.AccountService(manager);
        private _userLoggedInCallBack: UserLoggedInCallBack;

        constructor(userLoggedInCallBack: UserLoggedInCallBack)
        {
            this.SetGlobalHandlers();
            this._userLoggedInCallBack = userLoggedInCallBack;
            this.LoginButton.click(e => this.LoginButton_ClickHandler(e));
            this.RegisterButton.click(e => this.RegisterButton_ClickHandler(e));
        }

        private SetGlobalHandlers(): void 
        {
            var me = this;
            $(document).ajaxError(function (event, jqxhr, settings, thrownError)
            {
                if (!(settings.url.toLowerCase().indexOf("/account/login") > 0))
                {
                    return;
                }
                
                me.ErrorList.html("");
                for (var key in jqxhr.responseJSON)
                {
                    me.ErrorList.append('<p style="color:red">' + jqxhr.responseJSON[key] + '</p>');
                }
            });

            $(document).ajaxSuccess(function (event, xhr, settings)
            {
                if (!(settings.url.toLowerCase().indexOf("/account/login") > 0))
                {
                    return;
                }
                
                //me._userLoggedInCallBack();
                //window.location.href = "/";
            });
        }

        public RegisterButton_ClickHandler(e: JQueryEventObject): any
        {
            e.preventDefault();
            var info: RegisterInfo = new RegisterInfo();
            info.UserName = this.UserNameTextBox.val();
            info.Password = this.PasswordTextBox.val();
            info.ConfirmPassword = this.ConfirmPasswordTextBox.val();
            info.Email = this.EmailTextBox.val();


            var handler: HttpServiceHandler = new HttpServiceHandler();
            handler.HandleResponse = (r: HttpResponse): void =>
            {
                console.log("User has been registered");

                var loginInfo: LoginInfo = new LoginInfo();
                loginInfo.UserName = info.UserName;
                loginInfo.Password = info.Password;

                console.log("initiating login");

                this._service.Login(loginInfo);
                
                this._userLoggedInCallBack();
            };

            this._service.Register(info, handler);
        }

        LoginButton_ClickHandler(e: JQueryEventObject): any
        {
            
            var info: LoginInfo = new LoginInfo();
            info.UserName = this.UserNameTextBox.val();
            info.Password = this.PasswordTextBox.val();

            this._service.Login(info);
            this._userLoggedInCallBack();

        }
    }
}


