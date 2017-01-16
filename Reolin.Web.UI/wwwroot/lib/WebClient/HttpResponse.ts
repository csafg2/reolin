
module Reolin.Web.Client
{
    export class HttpResponse
    {
        private _statusCode: number;
        private _responseHeaders: { [key: string]: string };
        private _responseBody: any;


        get StatusCode(): number
        {
            return this._statusCode;
        }

        set StatusCode(code: number)
        {
            this._statusCode = code;
        }


        get ResponseHeaders(): { [key: string]: string }
        {
            return this._responseHeaders;
        }

        set ResponseHeaders(headers: { [key: string]: string })
        {
            this._responseHeaders = headers;
        }


        get ResponseBody(): any
        {
            return this._responseBody;
        }

        set ResponseBody(body: any)
        {
            this._responseBody = body;
        }
    }
}
