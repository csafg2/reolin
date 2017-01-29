module Reolin.Web.Client
{

    export class AccountService
    {
        private _jwtManager: IJwtManager;

        constructor(jwtManager: IJwtManager)
        {
            this._jwtManager = jwtManager;
        }

        public Register(registerInfo: RegisterInfo, handler: HttpServiceHandler): void
        {
            // send off a request to register the user
            var service: HttpService = new HttpService();

            service.Post(URLs.RegisterAccount, registerInfo, null, 2, true, handler);
        }

        public Login(info: LoginInfo): void
        {
            var token: JwtSecurityToken = manager.IssueJwt(info);
            //TODO: safely redirect to dashboard page
            //console.log("user registered and logged in")
        }

        public Relogin(): void
        {
            var jwt: JwtSecurityToken = this._jwtManager.GetLocalJwt();
            if (jwt === null)
            {
                throw new Error("oldJwt can not be null");
            }

            this._jwtManager.ProvideJwtbyOldJwt(jwt);
        }
    }


}