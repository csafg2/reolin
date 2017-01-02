module Reolin.Web.Client
{
    export class LoginManager
    {
        private _provider: IJwtProvider;

        constructor(provider: IJwtProvider)
        {
            this._provider = provider;
        }

        Login(loginInfo: LoginInfo): void
        {
            if (loginInfo === null || !loginInfo.IsValid())
            {
                throw new Error("login info can not be null");
            }
            
            this._provider.ProvideJwtByLoginInfo(loginInfo);
        }
    }
}