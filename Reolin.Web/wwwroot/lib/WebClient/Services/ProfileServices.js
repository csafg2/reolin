var Net = Reolin.Web.Client;
var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var UI;
        (function (UI) {
            var Services;
            (function (Services) {
                class ServiceResult {
                }
                Services.ServiceResult = ServiceResult;
                class FailedServiceResult extends ServiceResult {
                }
                Services.FailedServiceResult = FailedServiceResult;
                class SucceededServiceResult extends ServiceResult {
                }
                Services.SucceededServiceResult = SucceededServiceResult;
                class EntityService {
                    constructor() {
                        this.HttpService = new Net.HttpService();
                    }
                }
                Services.EntityService = EntityService;
                class ProfileService extends EntityService {
                    constructor() {
                        super();
                        this.AuthService = new Net.AuthenticatedHttpServiceProvider(manager, () => {
                            alert("auth error! redirecting to login page.");
                            window.location.href = "/account/login";
                        });
                    }
                    CreatePersonal(info, handlers) {
                        this.AuthService.Post(UrlSource.CreatePersonalProfile, info, {}, 2, true, handlers);
                    }
                    CreateWorkProfile(info, handlers) {
                        this.AuthService.Post(UrlSource.CreateWorkProfile, info, {}, 2, true, handlers);
                    }
                    Find(model, handler) {
                        var httpService = new Net.HttpService();
                        httpService.GetWithData(UrlSource.MainSearch, {}, 2, true, model, handler);
                    }
                    GetBasicInfo(profileId, handler) {
                        var httpService = new Net.HttpService();
                        httpService.GetWithData(UrlSource.GetBasicProfileInfo, {}, 2, true, { id: profileId }, handler);
                    }
                    GetLatestComments(profileId, handler) {
                        this.AuthService.GetWithData(UrlSource.GetComments, {}, 3, true, { id: profileId }, handler);
                    }
                    GetLocation(profileId, handler) {
                        this.HttpService.GetWithData(UrlSource.GetLocation, {}, 2, true, { id: profileId }, handler);
                    }
                    GetTags(profileId, handler) {
                        var httpService = new Net.HttpService();
                        httpService.GetWithData(UrlSource.GetTags, {}, 2, true, { id: profileId }, handler);
                    }
                    GetPhoneNumber(profileId, handler) {
                        var httpService = new Net.HttpService();
                        httpService.GetWithData(UrlSource.GetPhoneNumbers, {}, 2, true, { id: profileId }, handler);
                    }
                    GetRelatedType(profileId, handler) {
                        var httpService = new Net.HttpService();
                        httpService.GetWithData(UrlSource.GetRelatedType, {}, 2, true, { id: profileId }, handler);
                    }
                    SendRelateRequest(model, handler) {
                        this.AuthService.Post(UrlSource.AddRelate, model, {}, 2, true, handler);
                    }
                    GetImageCategories(id, handler) {
                        //var httpService = new Net.HttpService();
                        this.HttpService.GetWithData(UrlSource.GetImageCategories, {}, 2, true, { id: id }, handler);
                    }
                    GetImageGalleryItems(id, handler) {
                        this.HttpService.GetWithData(UrlSource.GetImages, {}, 2, true, { id: id }, handler);
                    }
                    GetRequestRelatedProfiles(id, handler) {
                        this.HttpService.GetWithData(UrlSource.GetRequestRelated, {}, 2, true, { id: id }, handler);
                    }
                    GetCertificates(id, handler) {
                        this.HttpService.GetWithData(UrlSource.GetCertificates, {}, 2, true, { id: id }, handler);
                    }
                    AddTag(data, handler) {
                        this.AuthService.Post(UrlSource.AddTag, data, {}, 2, true, handler);
                    }
                    AddRelatedType(data, handler) {
                        this.AuthService.Post(UrlSource.AddRelatedType, data, {}, 2, true, handler);
                    }
                    AddImageCategory(data, handler) {
                        this.AuthService.Post(UrlSource.AddImageCategory, data, {}, 2, true, handler);
                    }
                    DeleteRelationRequest(id, handler) {
                        this.AuthService.Post(UrlSource.DeleteRelationRequest, { id: id }, {}, 2, true, handler);
                    }
                    ConfirmRelationRequest(id, handler) {
                        this.AuthService.Post(UrlSource.ConfirmRelationRequest, { id: id }, {}, 2, true, handler);
                    }
                    AddImage(data, handler) {
                        var formData = this.HttpService.CreateFormData(data);
                        var options = {
                            processData: false,
                            contentType: false,
                        };
                        this.AuthService.Post(UrlSource.AddImage, formData, {}, 2, true, handler, options);
                    }
                    AddComment(data, handler) {
                        this.AuthService.Post(UrlSource.AddComment, data, {}, 2, true, handler);
                    }
                    AddCertificate(data, handler) {
                        this.AuthService.Post(UrlSource.AddCertificate, data, {}, 2, true, handler);
                    }
                }
                Services.ProfileService = ProfileService;
                class RegisterProfileModel {
                }
                Services.RegisterProfileModel = RegisterProfileModel;
                class SearchProfileModel {
                }
                Services.SearchProfileModel = SearchProfileModel;
            })(Services = UI.Services || (UI.Services = {}));
        })(UI = Web.UI || (Web.UI = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
// TODO: remove this in production (for test purposes)
var Service = Reolin.Web.UI.Services;
var profileService = new Service.ProfileService();
