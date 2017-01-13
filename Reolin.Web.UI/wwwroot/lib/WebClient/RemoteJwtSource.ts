
module Reolin.Web.Client
{
    export class RemoteJwtSource implements IJwtSource
    {
        private _exhangeUrl: string;
        private _getJwtUrl: string;

        constructor(exhangeUrl: string, getJwtUrl: string)
        {
            // TODO: add argument validation
            //if (!exhangeUrl)
            //{
            //    throw new Error("exhangeUrl can not be null");
            //}

            this._exhangeUrl = exhangeUrl;
            this._getJwtUrl = getJwtUrl;
        }

        ExchangeJwt(oldJwt: JwtSecurityToken): JwtSecurityToken
        {
            var service = new HttpService();
            var headers: { [key: string]: string } = {};
            headers["Authorization"] = "bearer " + oldJwt.Token;
            var handler: HttpServiceHandler = new HttpServiceHandler();
            var result: JwtSecurityToken = null;
            handler.HandleResponse = (response: HttpResponse) =>
            {
                result = JwtSecurityToken.Parse(response.ResponseBody.newToken);
            }
            var response = service.MakeRequest("POST", this._exhangeUrl, null, headers, 2, handler);
            return result;
        }

        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken
        {
            var service = new HttpService();
            var requestData = { UserName: loginInfo.UserName, Password: loginInfo.Password };
            var handler = new HttpServiceHandler();
            var result: JwtSecurityToken = null;

            handler.HandleResponse = (response: HttpResponse) =>
            {
                result = JwtSecurityToken.Parse(response.ResponseBody.accessToken);
            }

            service.MakeRequest("POST", getUrl, requestData, null, 3, handler);
            return result;
        }
    }
}