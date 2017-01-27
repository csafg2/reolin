
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
            handler.HandleResponse = (response: HttpResponse): void =>
            {
                result = JwtSecurityToken.Parse(response.ResponseBody.newToken);
            };

            //handler.HandleError = (response: HttpResponse): void =>
            //{
            //    //console.log(response.Error);
            //};

            service.MakeRequest("POST", this._exhangeUrl, null, headers, 2, handler, false);
            return result;
        }

        IssueJwt(loginInfo: LoginInfo): JwtSecurityToken
        {
            var service = new HttpService();
            var requestData = { UserName: loginInfo.UserName, Password: loginInfo.Password };
            var handler = new HttpServiceHandler();
            var result: JwtSecurityToken = null;

            handler.HandleResponse = (response: HttpResponse): void =>
            {
                result = JwtSecurityToken.Parse(response.ResponseBody.accessToken);
            };
            handler.HandleError = (r: HttpResponse): void =>
            {
                //alert(r.Error);
              //  throw r.Error;
//                console.log(r.StatusCode);
            };

            service.MakeRequest("POST", this._getJwtUrl, requestData, null, 3, handler, false);
            return result;
        }
    }
}