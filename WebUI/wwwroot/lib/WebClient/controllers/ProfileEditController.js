var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                class ProfileEditController {
                    constructor() {
                        this.ViewingProfileId = $("#ViewingProfileId");
                        this.SaveNewTagButton = $("#SaveNewTagButton");
                        this.NewCertificateButton = $("#NewCertificateButton");
                        this.AddRelatedTypeButton = $("#AddRelatedTypeButton");
                        this.NewImageCategoryButton = $("#NewImageCategoryButton");
                        this.SaveImageButton = $("#SaveImageButton");
                        this.ProfileName = $("#ProfileName");
                        this.LikeCount = $("#LikeCount");
                        this.CityCountryLabel = $("#CityAndCountryLabel");
                        this.CommentButton = $("#CommentButton");
                        this._service = new Reolin.Web.UI.Services.ProfileService();
                    }
                    Initialize() {
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
                    SetCertificates() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in SetCerticates" + r.Error);
                        handler.HandleResponse = (r) => {
                            var markUp = $("#CertificateItemTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#CertificateList");
                        };
                        this._service.GetCertificates(this.ViewingProfileId.val(), handler);
                    }
                    CommentButton_ClickHandler(e) {
                    }
                    SetRelationRequests() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => console.log(r);
                        handler.HandleResponse = r => {
                            var markUp = $("#RelationRequestTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#relationRequestList");
                        };
                        this._service.GetRequestRelatedProfiles(this.ViewingProfileId.val(), handler);
                    }
                    SaveImageButton_ClickHandler(e) {
                        if (IsNullOrEmpty($('#ImageFile').prop('files')[0])) {
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
                    SetTags() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => console.log(r);
                        handler.HandleResponse = r => {
                            this.AddToTagsList(r);
                            this.AddToImageTagsList(r);
                        };
                        this._service.GetTags(this.ViewingProfileId.val(), handler);
                    }
                    AddToTagsList(r) {
                        var markUp = $("#TagItemTemplate");
                        renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#TagItemList");
                    }
                    AddToImageTagsList(r) {
                        var markUp = $("#TagOptionsTemplate");
                        renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#TagOptions");
                    }
                    SetImageCategories() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleResponse = r => {
                            var markUp = $("#ImageCategoryTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#ImageCategoryList");
                        };
                        this._service.GetImageCategories(this.ViewingProfileId.val(), handler);
                    }
                    NewImageCategoryButton_ClickHandler(e) {
                        var text = $("#NewImagCategoryTextBox").val();
                        if (IsNullOrEmpty(text)) {
                            return;
                        }
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleResponse = r => alert('done, refresh to see changes');
                        handler.HandleError = r => console.log(r.Error);
                        this._service.AddImageCategory({ profileId: this.ViewingProfileId.val(), name: text }, handler);
                    }
                    SetRelatedType() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => console.log(r);
                        handler.HandleResponse = r => {
                            var markUp = $("#relatedTypeTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#relatedTypeList");
                        };
                        this._service.GetRelatedType(this.ViewingProfileId.val(), handler);
                    }
                    AddRelatedTypeButton_ClickHandler(e) {
                        var text = $("#NewRelatedTypeTextBox").val();
                        if (IsNullOrEmpty(text)) {
                            alert('invalid related type');
                        }
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => console.log(r.ResponseText);
                        handler.HandleResponse = r => {
                            alert('done');
                        };
                        var profileId = this.ViewingProfileId.val();
                        this._service.AddRelatedType({ profileId: profileId, type: text }, handler);
                    }
                    NewCertificateButton_ClickHandler(e) {
                        var data = {
                            profileId: this.ViewingProfileId.val(),
                            year: $("#CertrificationYear").val(),
                            description: $("#CertificateDescription").val()
                        };
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleResponse = r => console.log(r.ResponseBody);
                        this._service.AddCertificate(data, handler);
                    }
                    SaveNewTagButton_ClickHandler(e) {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log(r);
                        handler.HandleResponse = (r) => {
                            alert('done, to view new tags refresh the page.');
                        };
                        var tagText = $("#tagName").val();
                        var id = this.ViewingProfileId.val();
                        this._service.AddTag({ profileId: id, tag: tagText }, handler);
                    }
                    SetInfo() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController");
                        handler.HandleResponse = (r) => {
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
                    SetDynamicHandlers() {
                        var me = this;
                        $('body').on('click', 'a.deleteRelationRequestButton', function () {
                            me.DeleteRelationRequest($(this));
                        });
                        $('body').on('click', 'a.ConfirmRelatinRequest', function () {
                            var id = parseInt($(this).attr('data-id'));
                            me.ConfirmRelationRequest(id);
                        });
                    }
                    ConfirmRelationRequest(id) {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => alert('error occured while tryig to confirm');
                        handler.HandleResponse = r => alert('successfully confirm, refresh to view results');
                        this._service.ConfirmRelationRequest(id, handler);
                    }
                    DeleteRelationRequest(button) {
                        var itemId = parseInt(button.attr('data-id'));
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => alert('error occured while tryig to delete');
                        handler.HandleResponse = r => alert('successfully deleted, refresh to view results');
                        this._service.DeleteRelationRequest(itemId, handler);
                    }
                    static Start() {
                        var controller = new ProfileEditController();
                        controller.Initialize();
                    }
                }
                Controllers.ProfileEditController = ProfileEditController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
Reolin.Web.Client.Controllers.ProfileEditController.Start();
