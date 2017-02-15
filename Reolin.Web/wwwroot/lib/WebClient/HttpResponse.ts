
module Reolin.Web.Client
{
    export class HttpResponse
    {
        private _statusCode: number;
        private _responseHeaders: { [key: string]: string };
        private _responseBody: any;
        private _error: string;
        private _text: string;

        get Error(): string
        {
            return this._error;
        }

        set Error(error: string)
        {
            this._error = error;
        }

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


        get ResponseText(): string {
            return this._text;

        }


        set ResponseText(value: string) {
            this._text = value;
        }
    }
}
