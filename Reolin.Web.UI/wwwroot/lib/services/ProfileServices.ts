/// <reference path="../webclient/httpservice.ts" />

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
        
    }

    export class ProfileService extends EntityService
    {
        public Create(profile): void
        {
        }
    }
}