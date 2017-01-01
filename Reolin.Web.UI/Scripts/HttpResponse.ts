module Reolin.Web.Client {
    export class HttpResponse {
        private _statusCode: number;
        private _responseHeaders: { [key: string]: string };
        private _responseBody: string;


        get StatusCode(): number {
            return this._statusCode;
        }

        set StatusCode(code: number) {
            this._statusCode = code;
        }


        get ResponseHeaders(): { [key: string]: string } {
            return this._responseHeaders;
        }
        
        set ResponseHeaders(headers: { [key: string]: string }) {
            this._responseHeaders = headers;
        }


        get ResponseBody(): string {
            return this._responseBody;
        }

        set ResponseBody(body: string) {
            this._responseBody = body;
        }
    }
}
