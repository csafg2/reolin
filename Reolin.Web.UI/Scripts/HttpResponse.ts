module Reolin.Web.UI {
    export class HttpResponse {
        _statusCode: number;
        _responseHeaders: { [key: string]: string };
        _responseBody: string;
    }
}
