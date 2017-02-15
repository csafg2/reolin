module Reolin.Web.Client
{

    export class RegisterInfo
    {
        UserName: string;
        Password: string;
        ConfirmPassword: string;
        Email: string;
        PhoneNumber: string;
    }


    export class LoginInfo
    {
        private _userName: string;
        private _password: string;

        get UserName(): string
        {
            return this._userName;
        }

        set UserName(userName: string)
        {
            this._userName = userName;
        }

        get Password(): string
        {
            return this._password;
        }

        set Password(password: string)
        {
            this._password = password;
        }

        public IsValid(): boolean
        {
            if (!this.UserName || !this.Password)
            {
                return false;
            }

            return true;
        }
    }

}