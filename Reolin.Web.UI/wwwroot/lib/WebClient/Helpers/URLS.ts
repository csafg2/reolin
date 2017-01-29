module Reolin.Web.Client
{

    export class LocalURLs
    {
        public static RegisterAccount: string = "http://localhost:6987/account/register";
        public static ExhangeTokenUrl: string = "http://localhost:6987/account/ExchangeToken";
        public static GetTokenUrl: string = "http://localhost:6987/account/Login";
    }

    export class URLs
    {
        public static RegisterAccount: string = "http://178.63.55.123/account/register";
        public static ExhangeTokenUrl: string = "http://178.63.55.123/account/ExchangeToken";
        public static GetTokenUrl: string = "http://178.63.55.123/account/Login";

        public static DashboardLocation: string = "/Account/Dashboard.html";
    }
}