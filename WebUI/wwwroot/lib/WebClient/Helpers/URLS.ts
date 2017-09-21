module Reolin.Web.Client
{
    export class URLs
    {
        public static Prefix: string = "http://localhost:6987/";

        public static CountryList: string = URLs.Prefix + "GeoService/Countries";

        public static RegisterAccount: string = URLs.Prefix + "account/register";
        public static ExhangeTokenUrl: string = URLs.Prefix + "account/ExchangeToken";
        public static GetTokenUrl: string = URLs.Prefix + "account/Login";
        
        public static GetByTag: string = "http://178.63.55.123/GET/Profile/GetByTag"
        public static AddDescription: string = URLs.Prefix + "/Profile/AddDescription";
        public static AddImage: string = URLs.Prefix + "/Profile/AddImage";
        public static AddLike: string = URLs.Prefix + "/User/LikeProfile/";
        public static CreateWorkProfile: string = URLs.Prefix + "/Profile/CreateWork";
        public static CreatePersonalProfile: string = URLs.Prefix + "/Profile/CreatePersonal";
        public static GetProfileInfo: string = URLs.Prefix + "/Profile/GetInfo/";
        
        public static GetJobCategories: string = URLs.Prefix + "JobCategory/JobCateogries";
        public static GetSubJobCategories: string = URLs.Prefix + "JobCategory/SubJobCategories";

        public static GetCity: string = URLs.Prefix + "GeoService/Cities";
        public static GetCountry: string = URLs.Prefix + "GeoService/Countries";
       
        public static MainSearch: string = URLs.Prefix + "Profile/Find";

        public static QueryUserProfiles: string = URLs.Prefix + "User/QueryProfiles";

        public static GetBasicProfileInfo: string = URLs.Prefix + "Profile/BasicInfo";

        public static GetComments: string = URLs.Prefix + "Profile/LatestComments";
        public static GetTags: string = URLs.Prefix + "Profile/GetTags";

        public static GetPhoneNumbers: string = URLs.Prefix + "Profile/PhoneNumbers";
        public static GetRelatedType: string = URLs.Prefix + "Profile/RelatedTypes";

        public static AddRelate: string = URLs.Prefix + "Profile/AddRelation";

        public static GetImageCategories: string = URLs.Prefix + "Profile/GetImageCategories";
        public static GetImages: string = URLs.Prefix + "Profile/Images";


        public static GetRequestRelated: string = URLs.Prefix + "Profile/RequestRelatedProfiles";

        public static GetCertificates: string = URLs.Prefix + "Profile/Certificates";

        public static AddTag: string = URLs.Prefix + "Profile/AddTag";

        public static AddRelatedType: string = URLs.Prefix + "Profile/AddRelatedType";

        public static AddImageCategory: string = URLs.Prefix + "Profile/AddImageCategory";

        public static DeleteRelationRequest: string = URLs.Prefix + "Profile/DeleteRelationRequest";
        public static ConfirmRelationRequest: string = URLs.Prefix + "Profile/ConfirmRelationRequest";
        public static AddComment: string = URLs.Prefix + "User/AddComment";

        public static AddCertificate: string = URLs.Prefix + "Profile/AddCertificate";

        public static GetLocation: string = URLs.Prefix + "Profile/Location";
    }

    //export class URLs
    //{
    //    public static Host: string = "http://178.63.55.123/";
    //    public static RegisterAccount: string = "http://178.63.55.123/account/register";
    //    public static ExhangeTokenUrl: string = "http://178.63.55.123/account/ExchangeToken";
    //    public static GetTokenUrl: string = "http://178.63.55.123/account/Login";
        
    //    public static GetByTag: string = "http://178.63.55.123/GET/Profile/GetByTag"
    //    public static AddDescription: string = "http://178.63.55.123/Profile/AddDescription";
    //    public static AddImage: string = "http://178.63.55.123/Profile/AddImage";
    //    public static AddLike: string = "http://178.63.55.123/User/LikeProfile/";
    //    public static CreateWorkProfile: string = "http://178.63.55.123/Profile/CreateWork";
    //    public static CreatePersonalProfile: string = "http://178.63.55.123/Profile/CreatePersonal";
    //    public static GetProfileInfo: string = "http://178.63.55.123/Profile/GetInfo/";

    //    public static DashboardLocation: string = "http://178.63.55.123/Account/Dashboard.html";

    //    public static GetJobCategories: string = "http://178.63.55.123/JobCategory/JobCateogries";
    //    public static GetSubJobCategories: string = "http://178.63.55.123/JobCategory/SubJobCategories";

    //    public static GetCity: string = "http://178.63.55.123/GeoService/Cities";
    //    public static GetCountry: string = "http://178.63.55.123/GeoService/Countries";

    //    public static MainSearch: string = "http://178.63.55.123/Profile/Find";

    //    public static QueryUserProfiles: string = "http://178.63.55.123/User/QueryProfiles";
    //    public static GetBasicProfileInfo: string = "http://178.63.55.123/Profile/BasicInfo";

    //    public static GetComments: string = "http://178.63.55.123/Profile/LatestComments";
    //    public static GetTags: string = "http://178.63.55.123/Profile/GetTags";

    //    public static GetPhoneNumbers: string = "http://178.63.55.123/Profile/PhoneNumbers";

    //    public static GetRelatedType: string = "http://178.63.55.123/Profile/RelatedTypes";
    //    public static AddRelate: string = "http://178.63.55.123/Profile/AddRelation";

    //    public static GetImageCategories: string = "http://178.63.55.123/Profile/GetImageCategories";

    //    public static GetImages: string = "http://178.63.55.123/Profile/Images";

    //    public static GetRequestRelated: string = "http://178.63.55.123/Profile/RequestRelatedProfiles";


    //    public static GetCertificates: string = "http://178.63.55.123/Profile/Certificates";

    //    public static AddTag: string = "http://178.63.55.123/Profile/AddTag";

    //    public static AddRelatedType: string = "http://178.63.55.123/Profile/AddRelatedType";

    //    public static AddImageCategory: string = "http://178.63.55.123/Profile/AddImageCategory";

    //    public static DeleteRelationRequest: string = "http://178.63.55.123/Profile/DeleteRelationRequest";
    //    public static ConfirmRelationRequest: string = "http://178.63.55.123/Profile/ConfirmRelationRequest";

    //    public static AddComment: string = "http://178.63.55.123/User/AddComment";

    //    public static AddCertificate: string = "http://178.63.55.123/Profile/AddCertificate";

    //    public static GetLocation: string = "http://178.63.55.123/Profile/Location";
    //}
}