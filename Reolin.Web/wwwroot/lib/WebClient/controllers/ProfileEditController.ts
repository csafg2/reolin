
module Reolin.Web.Client.Controllers
{

    declare var renderEngine;

    export class ProfileEditController
    {
        ViewingProfileId: JQuery = $("#ViewingProfileId");
        SaveNewTagButton: JQuery = $("#SaveNewTagButton");
        NewCertificateButton: JQuery = $("#NewCertificateButton");
        AddRelatedTypeButton: JQuery = $("#AddRelatedTypeButton");
        NewImageCategoryButton: JQuery = $("#NewImageCategoryButton");
        SaveImageButton: JQuery = $("#SaveImageButton");
        ProfileName: JQuery = $("#ProfileName");
        LikeCount: JQuery = $("#LikeCount");
        CityCountryLabel: JQuery = $("#CityAndCountryLabel");
        CommentButton: JQuery = $("#CommentButton");

        _service: Reolin.Web.UI.Services.ProfileService = new Reolin.Web.UI.Services.ProfileService();

        public Initialize(): void 
        {
            this.SetDynamicHandlers();
            this.SetRelationRequests();
            this.SetInfo();
            this.SetRelatedType();
            this.SetImageCategories();
            this.SetTags();
            this.SetCertificates();
            this.CommentButton.click(e => this.CommentButton_ClickHandler(e));
            this.SaveImageButton.click(e => this.SaveImageButton_ClickHandler(e));
            this.SaveNewTagButton.click(e => this.SaveNewTagButton_ClickHandler(e));
            this.NewCertificateButton.click(e => this.NewCertificateButton_ClickHandler(e));
            this.AddRelatedTypeButton.click(e => this.AddRelatedTypeButton_ClickHandler(e));
            this.NewImageCategoryButton.click(e => this.NewImageCategoryButton_ClickHandler(e));
        }


        public SetCertificates(): void 
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in SetCerticates" + r.Error);
            handler.HandleResponse = (r: HttpResponse) => 
            {
                var markUp = $("#CertificateItemTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#CertificateList");
            };

            this._service.GetCertificates(this.ViewingProfileId.val(), handler);
        }

        public CommentButton_ClickHandler(e: Event)
        {

        }



        public SetRelationRequests()
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => console.log(r);
            handler.HandleResponse = r =>
            {
                var markUp = $("#RelationRequestTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#relationRequestList");
            };
            this._service.GetRequestRelatedProfiles(this.ViewingProfileId.val(), handler);
        }

        public SaveImageButton_ClickHandler(e: Event)
        {
            if (IsNullOrEmpty($('#ImageFile').prop('files')[0]))
            {
                alert('select a file');
                return;
            }
            var data = {
                subject: $("#ImageSubject").val(),
                categoryId: $("#ImageCategoryList").val(),
                description: $("#ImageDescription").val(),
                tagIds: $("#TagOptions").val(),
                profileId: this.ViewingProfileId.val(),
                file: $('#ImageFile').prop('files')[0]
            };

            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => console.log(r);
            handler.HandleResponse = r => alert('done');
            this._service.AddImage(data, handler);
        }

        public SetTags(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => console.log(r);
            handler.HandleResponse = r =>
            {
                this.AddToTagsList(r);
                this.AddToImageTagsList(r);
            };
            this._service.GetTags(this.ViewingProfileId.val(), handler);
        }

        public AddToTagsList(r: HttpResponse): void 
        {
            var markUp = $("#TagItemTemplate");
            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#TagItemList");
        }

        public AddToImageTagsList(r: HttpResponse): void
        {
            var markUp = $("#TagOptionsTemplate");
            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#TagOptions");
        }

        public SetImageCategories(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleResponse = r =>
            {
                var markUp = $("#ImageCategoryTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#ImageCategoryList");
            };
            this._service.GetImageCategories(this.ViewingProfileId.val(), handler);
        }

        public NewImageCategoryButton_ClickHandler(e: Event)
        {
            var text: string = $("#NewImagCategoryTextBox").val();
            if (IsNullOrEmpty(text))
            {
                return;
            }
            var handler = new Net.HttpServiceHandler();
            handler.HandleResponse = r => alert('done, refresh to see changes');
            handler.HandleError = r => console.log(r.Error);
            this._service.AddImageCategory({ profileId: this.ViewingProfileId.val(), name: text }, handler);
        }


        public SetRelatedType(): void 
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => console.log(r);

            handler.HandleResponse = r => 
            {
                var markUp = $("#relatedTypeTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#relatedTypeList");
            }
            this._service.GetRelatedType(this.ViewingProfileId.val(), handler);
        }

        public AddRelatedTypeButton_ClickHandler(e: Event)
        {
            var text = $("#NewRelatedTypeTextBox").val();
            if (IsNullOrEmpty(text))
            {
                alert('invalid related type');
            }
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => console.log(r.ResponseText);
            handler.HandleResponse = r => 
            {
                alert('done');
            };
            var profileId = this.ViewingProfileId.val();
            this._service.AddRelatedType({ profileId: profileId, type: text }, handler);
        }

        public NewCertificateButton_ClickHandler(e: Event)
        {
            var data =
                {
                    profileId: this.ViewingProfileId.val(),
                    year: $("#CertrificationYear").val(),
                    description: $("#CertificateDescription").val()
                };

            var handler = new Net.HttpServiceHandler();
            handler.HandleResponse = r => console.log(r.ResponseBody);
            this._service.AddCertificate(data, handler);

        }

        public SaveNewTagButton_ClickHandler(e: Event)
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log(r);
            handler.HandleResponse = (r: HttpResponse) => 
            {
                alert('done, to view new tags refresh the page.');
            };
            var tagText = $("#tagName").val();
            var id = this.ViewingProfileId.val();
            this._service.AddTag({ profileId: id, tag: tagText }, handler);
        }


        public SetInfo(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var result = r.ResponseBody;
                this.CityCountryLabel.text(result.city + "/ " + result.country);
                this.LikeCount.text(result.likeCount);
                this.ProfileName.text(result.name);

                $("#ProfileNameTextBox").val(result.name);
                $("#CityTextBox").val(result.city);
                $("#CountryText").val(result.country);
            };

            this._service.GetBasicInfo(this.ViewingProfileId.val(), handler);
        }


        public SetDynamicHandlers(): void
        {
            var me = this;
            $('body').on('click', 'a.deleteRelationRequestButton', function ()
            {
                me.DeleteRelationRequest($(this));
            });

            $('body').on('click', 'a.ConfirmRelatinRequest', function ()
            {
                var id: number = parseInt($(this).attr('data-id'));
                me.ConfirmRelationRequest(id);
            });
        }

        public ConfirmRelationRequest(id: number)
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => alert('error occured while tryig to confirm');
            handler.HandleResponse = r => alert('successfully confirm, refresh to view results');
            this._service.ConfirmRelationRequest(id, handler);
        }

        public DeleteRelationRequest(button: JQuery)
        {
            var itemId = parseInt(button.attr('data-id'));
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = r => alert('error occured while tryig to delete');
            handler.HandleResponse = r => alert('successfully deleted, refresh to view results');
            this._service.DeleteRelationRequest(itemId, handler);
        }

        public static Start()
        {
            var controller: ProfileEditController = new ProfileEditController();
            controller.Initialize();
        }
    }
}

Reolin.Web.Client.Controllers.ProfileEditController.Start();