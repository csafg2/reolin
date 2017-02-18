
module Reolin.Web.Client.Controllers
{

    declare var renderEngine;

    export class ProfileViewController
    {
        CurrentProfile: JQuery = $("#CurrentProfileId");
        ProfileName: JQuery = $("#ProfileName");
        LikeCount: JQuery = $("#LikeCount");
        CityCountryLabel: JQuery = $("#CityAndCountryLabel");
        ViewingProfileId: JQuery = $("#ViewingProfileId");
        SendRelateRequestButton: JQuery = $("#SendRelateRequest");
        SendCommentButton: JQuery = $("#SendCommentButton");

        _service: Reolin.Web.UI.Services.ProfileService = new Reolin.Web.UI.Services.ProfileService();


        public static Start(): void 
        {
            console.log("in view controller");
            var controller: ProfileViewController = new ProfileViewController();
            controller.Initialize();
        }


        public Initialize(): void 
        {
            this.SetInfoBox();
            this.SetTags();
            this.SetPhoneNumber();
            this.SetRelatedTypes();
            this.SetImageCategories();
            this.SetImageGalleries();
            this.SetRelations();
            this.SetCertificates();
            this.SetRouteButton();
            this.SendRelateRequestButton.click(e => this.SendRelateRequestButton_clickHandler(e));
            this.SendCommentButton.click(e => this.SendCommentButton_ClickHandler(e));
        }

        public SetRouteButton(): void
        {
            var button = $("#routeButton");
            var setSourceHandler = new Net.HttpServiceHandler();
            setSourceHandler.HandleError = r => console.log(r);
            setSourceHandler.HandleResponse = r => 
            {
                var source = r.ResponseBody;
                button.attr('src-lat', source.latitude);
                button.attr('src-long', source.longitude);
            };

            this._service.GetLocation(this.CurrentProfile.val(), setSourceHandler);

            var setDestinationHandler = new Net.HttpServiceHandler();
            setDestinationHandler.HandleError = r => console.log(r);
            setDestinationHandler.HandleResponse = r => 
            {
                var dest = r.ResponseBody;
                button.attr('dest-lat', dest.latitude);
                button.attr('dest-long', dest.longitude);
            };

            this._service.GetLocation(this.ViewingProfileId.val(), setDestinationHandler);
            
            button.click(e =>
            {
                window.location.href =
                    '/'
                    + button.attr('src-lat')
                    + '/and/'
                    + button.attr('src-long')
                    + '/to/' + button.attr('dest-lat')
                    + '/and/' + button.attr('dest-long');
            });
        }

        public SendCommentButton_ClickHandler(e: Event): void
        {
            var data =
                {
                    ProfileId: this.ViewingProfileId.val(),
                    Message: $("#CommentText").val()
                };
            console.log(data);
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => console.log(r);
            handler.HandleResponse = r => alert('comment sent, and will be visible after has been approved');

            this._service.AddComment(data, handler);
        }

        public SetCertificates(): void 
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in SetCerticates" + r.Error);
            handler.HandleResponse = (r: HttpResponse) => 
            {
                var markUp = $("#certificateItemTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#certificateList");
            };

            this._service.GetCertificates(this.ViewingProfileId.val(), handler);
        }

        public SetRelations(): void 
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in " + r.Error);
            handler.HandleResponse = (r: HttpResponse) => 
            {
                var acceptedList = r.ResponseBody.filter(e => e.confirmed == true);

                var markUp = $("#relatedTemplate");
                renderEngine.tmpl(markUp, acceptedList).appendTo("#relationsList");



                //var listMarkUp = $("#relationRequestTemplate");
                //renderEngine.tmpl(listMarkUp, acceptedList).appendTo("#relationRequestList");
            };
            this._service.GetRequestRelatedProfiles(this.ViewingProfileId.val(), handler);
        }

        public SetImageGalleries(): void 
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in " + r.Error);
            handler.HandleResponse = (r: HttpResponse) => 
            {
                var markup = $("#imagesTemplate");
                // Compile the markup as a named template
                renderEngine.template("images", markup);

                // Render the template with the movies data
                renderEngine.tmpl("images", r.ResponseBody,
                    {
                        Host: UrlSource.Host,
                    }
                ).appendTo("#imageList");
            };

            this._service.GetImageGalleryItems(this.ViewingProfileId.val(), handler);
        }

        public SetImageCategories(): void 
        {
            var id = this.ViewingProfileId.val();
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log(r.Error);
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var markUp = $("#imageCategoryTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#imageCategoryList");
            };
            this._service.GetImageCategories(id, handler);
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