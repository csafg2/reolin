
module Reolin.Web.Client
{
    export class RemoteJwtSource implements IJwtSource
    {
        private _exhangeUrl: string;
        private _getJwtUrl: string;

        constructor(exhangeUrl: string, getJwtUrl: string)
        {
            if (IsNullOrEmpty(exhangeUrl, getJwtUrl))
            {
                throw new Error("exhangeUrl and getJwtUrl can not be null");
            }

            this._exhangeUrl = exhangeUrl;
            this._getJwtUrl = getJwtUrl;
        }

        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken
        {
            if (oldJwt === null)
            {
                throw new Error("OldJwt Can not be null");
            }

            var service: HttpService = new HttpService();
            var headers: { [key: string]: string } = {};
            headers["Authorization"] = "bearer " + oldJwt.Token;
            var handler: HttpServiceHandler = new HttpServiceHandler();
            var result: JwtSecurityToken = null;
            handler.HandleResponse = (response: HttpResponse): void =>
            {
                result = JwtSecurityToken.Parse(response.ResponseBody.newToken);
            };

            service.MakeRequest("POST", this._exhangeUrl, null, headers, 2, handler, false);
            return result;
        }

        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken
        {
            if (loginInfo === null)
            {
                throw new Error("loginInfo can not be null");
            }

            var service = new HttpService();
            var requestData = { UserName: loginInfo.UserName, Password: loginInfo.Password };
            var handler = new HttpServiceHandler();
            var result: JwtSecurityToken = null;

            handler.HandleResponse = (response: HttpResponse): void =>
            {
                result = JwtSecurityToken.Parse(response.ResponseBody.accessToken);
            }

            service.MakeRequest("POST", this._getJwtUrl, requestData, null, 3, handler, false);
            return result;
        }
    }
}