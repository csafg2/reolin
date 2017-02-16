﻿module Reolin.Web.Client
{

    export class LocalURLs
    {
        public static RegisterAccount: string = "http://localhost:6987/account/register";
        public static ExhangeTokenUrl: string = "http://localhost:6987/account/ExchangeToken";
        public static GetTokenUrl: string = "http://localhost:6987/account/Login";
        
        public static GetByTag: string = "http://178.63.55.123/GET/Profile/GetByTag"
        public static AddDescription: string = "http://localhost:6987//Profile/AddDescription";
        public static AddImage: string = "http://localhost:6987//Profile/AddImage";
        public static AddLike: string = "http://localhost:6987//User/LikeProfile/";
        public static CreateWorkProfile: string = "http://localhost:6987//Profile/CreateWork";
        public static CreatePersonalProfile: string = "http://localhost:6987//Profile/CreatePersonal";
        public static GetProfileInfo: string = "http://localhost:6987//Profile/GetInfo/";
        
        public static GetJobCategories: string = "http://localhost:6987/JobCategory/JobCateogries";
        public static GetSubJobCategories: string = "http://localhost:6987/JobCategory/SubJobCategories";

        public static GetCity: string = "http://localhost:6987/GeoService/Cities";
        public static GetCountry: string = "http://localhost:6987/GeoService/Countries";


        public static MainSearch: string = "http://localhost:6987/Profile/Find";

        public static QueryUserProfiles: string = "http://localhost:6987/User/QueryProfiles";
    }

    export class URLs
    {
        public static RegisterAccount: string = "http://178.63.55.123/account/register";
        public static ExhangeTokenUrl: string = "http://178.63.55.123/account/ExchangeToken";
        public static GetTokenUrl: string = "http://178.63.55.123/account/Login";
        
        public static GetByTag: string = "http://178.63.55.123/GET/Profile/GetByTag"
        public static AddDescription: string = "http://178.63.55.123/Profile/AddDescription";
        public static AddImage: string = "http://178.63.55.123/Profile/AddImage";
        public static AddLike: string = "http://178.63.55.123/User/LikeProfile/";
        public static CreateWorkProfile: string = "http://178.63.55.123/Profile/CreateWork";
        public static CreatePersonalProfile: string = "http://178.63.55.123/Profile/CreatePersonal";
        public static GetProfileInfo: string = "http://178.63.55.123/Profile/GetInfo/";

        public static DashboardLocation: string = "http://178.63.55.123/Account/Dashboard.html";


        public static GetJobCategories: string = "http://178.63.55.123/JobCategory/JobCateogries";
        public static GetSubJobCategories: string = "http://178.63.55.123/JobCategory/SubJobCategories";


        public static GetCity: string = "http://178.63.55.123/GeoService/Cities";
        public static GetCountry: string = "http://178.63.55.123/GeoService/Countries";

        public static MainSearch: string = "http://178.63.55.123/Profile/Find";

        public static QueryUserProfiles: string = "http://178.63.55.123/User/QueryProfiles";
    }
}