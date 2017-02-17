import Net = Reolin.Web.Client;


module Reolin.Web.UI.Services
{
    export class ServiceResult
    {

    }

    export class FailedServiceResult extends ServiceResult
    {

    }

    export class SucceededServiceResult extends ServiceResult
    {

    }

    export class EntityService
    {
        protected HttpService: Net.HttpService = new Net.HttpService();
        protected AuthService: Net.AuthenticatedHttpServiceProvider;
    }

    export class ProfileService extends EntityService
    {
        constructor()
        {
            super();

            this.AuthService = new Net.AuthenticatedHttpServiceProvider(manager, () =>
            {
                alert("auth error!");
                window.location.href = "/account/login";
            });
        }

        public CreatePersonal(info: RegisterProfileModel, handlers: Net.HttpServiceHandler): void
        {
            this.AuthService.Post(UrlSource.CreatePersonalProfile, info, {}, 2, true, handlers);
        }

        public CreateWorkProfile(info: RegisterProfileModel, handlers: Net.HttpServiceHandler)
        {
            this.AuthService.Post(UrlSource.CreateWorkProfile, info, {}, 2, true, handlers);
        }

        public Find(model: SearchProfileModel, handler: Net.HttpServiceHandler): void
        {
            var httpService = new Net.HttpService();
            httpService.GetWithData(UrlSource.MainSearch, {}, 2, true, model, handler);
        }

        public GetBasicInfo(profileId: number, handler: Net.HttpServiceHandler): void
        {
            var httpService = new Net.HttpService();
            httpService.GetWithData(UrlSource.GetBasicProfileInfo, {}, 2, true, { id: profileId }, handler);
        }

        public GetLatestComments(profileId: number, handler: Net.HttpServiceHandler)
        {
            this.AuthService.GetWithData(UrlSource.GetComments, {}, 3, true, { id: profileId }, handler);
        }

        public GetTags(profileId: number, handler: Net.HttpServiceHandler)
        {
            var httpService = new Net.HttpService();
            httpService.GetWithData(UrlSource.GetTags, {}, 2, true, { id: profileId }, handler);
        }

        public GetPhoneNumber(profileId: number, handler: Net.HttpServiceHandler)
        {
            var httpService = new Net.HttpService();
            httpService.GetWithData(UrlSource.GetPhoneNumbers, {}, 2, true, { id: profileId }, handler);

        }

        public GetRelatedType(profileId: number, handler: Net.HttpServiceHandler)
        {
            var httpService = new Net.HttpService();
            httpService.GetWithData(UrlSource.GetRelatedType, {}, 2, true, { id: profileId }, handler);

        }

        public SendRelateRequest(model: any, handler: Net.HttpServiceHandler)
        {
            var httpService = new Net.HttpService();
            this.AuthService.Post(UrlSource.AddRelate, model, {}, 2, true, handler);

        }
    }

    export class RegisterProfileModel
    {
        public Name: string;
        public Description: string;
        public City: string;
        public Country: string;
        public PhoneNumber: string;
        public Longitude: number;
        public Latitude: number;
        public JobCategoryId: number;
        public SubJobCategoryID : number;
    }
    
    export class SearchProfileModel
    {
        public JobCategoryId: number;
        public SubJobCategoryId: number;
        public SearchTerm: string;
        public Distance: number;
        public SourceLatitude: number;
        public SourceLongitude: number;
    }
}

// TODO: remove this in production (for test purposes)
import Service = Reolin.Web.UI.Services;

var profileService = new Service.ProfileService();