var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var URLs = (function () {
                function URLs() {
                }
                return URLs;
            }());
            URLs.Prefix = "http://localhost:6987/";
            URLs.RegisterAccount = URLs.Prefix + "account/register";
            URLs.ExhangeTokenUrl = URLs.Prefix + "account/ExchangeToken";
            URLs.GetTokenUrl = URLs.Prefix + "account/Login";
            URLs.GetByTag = "http://178.63.55.123/GET/Profile/GetByTag";
            URLs.AddDescription = URLs.Prefix + "/Profile/AddDescription";
            URLs.AddImage = URLs.Prefix + "/Profile/AddImage";
            URLs.AddLike = URLs.Prefix + "/User/LikeProfile/";
            URLs.CreateWorkProfile = URLs.Prefix + "/Profile/CreateWork";
            URLs.CreatePersonalProfile = URLs.Prefix + "/Profile/CreatePersonal";
            URLs.GetProfileInfo = URLs.Prefix + "/Profile/GetInfo/";
            URLs.GetJobCategories = URLs.Prefix + "JobCategory/JobCateogries";
            URLs.GetSubJobCategories = URLs.Prefix + "JobCategory/SubJobCategories";
            URLs.GetCity = URLs.Prefix + "GeoService/Cities";
            URLs.GetCountry = URLs.Prefix + "GeoService/Countries";
            URLs.MainSearch = URLs.Prefix + "Profile/Find";
            URLs.QueryUserProfiles = URLs.Prefix + "User/QueryProfiles";
            URLs.GetBasicProfileInfo = URLs.Prefix + "Profile/BasicInfo";
            URLs.GetComments = URLs.Prefix + "Profile/LatestComments";
            URLs.GetTags = URLs.Prefix + "Profile/GetTags";
            URLs.GetPhoneNumbers = URLs.Prefix + "Profile/PhoneNumbers";
            URLs.GetRelatedType = URLs.Prefix + "Profile/RelatedTypes";
            URLs.AddRelate = URLs.Prefix + "Profile/AddRelation";
            URLs.GetImageCategories = URLs.Prefix + "Profile/GetImageCategories";
            URLs.GetImages = URLs.Prefix + "Profile/Images";
            URLs.GetRequestRelated = URLs.Prefix + "Profile/RequestRelatedProfiles";
            URLs.GetCertificates = URLs.Prefix + "Profile/Certificates";
            URLs.AddTag = URLs.Prefix + "Profile/AddTag";
            URLs.AddRelatedType = URLs.Prefix + "Profile/AddRelatedType";
            URLs.AddImageCategory = URLs.Prefix + "Profile/AddImageCategory";
            URLs.DeleteRelationRequest = URLs.Prefix + "Profile/DeleteRelationRequest";
            URLs.ConfirmRelationRequest = URLs.Prefix + "Profile/ConfirmRelationRequest";
            URLs.AddComment = URLs.Prefix + "User/AddComment";
            URLs.AddCertificate = URLs.Prefix + "Profile/AddCertificate";
            URLs.GetLocation = URLs.Prefix + "Profile/Location";
            Client.URLs = URLs;
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
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
