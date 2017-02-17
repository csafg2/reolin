
module Reolin.Web.Client.Controllers
{
   
    declare var renderEngine;

    export class ProfileViewController
    {
        ProfileName: JQuery = $("#ProfileName");
        LikeCount: JQuery = $("#LikeCount");
        CityCountryLabel: JQuery = $("#CityAndCountryLabel");
        ViewingProfileId: JQuery = $("#ViewingProfileId");
        SendRelateRequestButton: JQuery = $("#SendRelateRequest");

        _service: Reolin.Web.UI.Services.ProfileService = new Reolin.Web.UI.Services.ProfileService();


        public static Start(): void 
        {
            var controller: ProfileViewController = new ProfileViewController();
            controller.Initialize();
        }


        public Initialize(): void 
        {
            this.SetInfoBox();
            this.SetTags();
            this.SetPhoneNumber();
            this.SetRelatedTypes();
            this.SendRelateRequestButton.click(e => this.SendRelateRequestButton_clickHandler(e));
        }

        public SendRelateRequestButton_clickHandler(e: Event): void
        {
            //Task < int > AddRelate(int sourceId, int targetId, DateTime date, string description, int relatedTypeId);
            var relatedTypeId = $("#type").val();
            var sourceId: number = parseInt($("#CurrentProfileId").val());
            var targetId: number = parseInt($("#ViewingProfileId").val());
            var date = $("#datePicker").val();
            var description = $("#explain").val();
            var handler = new Net.HttpServiceHandler();
            handler.HandleResponse = (r: HttpResponse) => alert("done");
            handler.HandleError = (r: HttpResponse) => console.log(r);
            this._service.SendRelateRequest(
                {
                    relatedTypeId: relatedTypeId,
                    sourceId: sourceId,
                    targetId: targetId,
                    date: date,
                    description: description
                }, handler);
        }

        public SetInfoBox(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var result = r.ResponseBody;
                this.CityCountryLabel.text(result.city + "/ " + result.country);
                this.LikeCount.text(result.likeCount);
                this.ProfileName.text(result.name);
            };

            this._service.GetBasicInfo(this.ViewingProfileId.val(), handler);
        }


        private SetTags(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController -- set tags");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var markUp = $("#secondTagTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#secondTagList");
            };

            this._service.GetTags(this.ViewingProfileId.val(), handler);

        }

        private SetPhoneNumber(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController -- set phoneNumber");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var markUp = $("#phoneNumberTemplateContainer");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#phoneNumberList");
            };

            this._service.GetPhoneNumber(this.ViewingProfileId.val(), handler);
        }
        
        public SetRelatedTypes(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController -- set relatedtype");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var markUp = $("#relatedTypeTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#type");
            };

            this._service.GetRelatedType(this.ViewingProfileId.val(), handler);
        }

    }
}
Reolin.Web.Client.Controllers.ProfileViewController.Start();