var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                class ProfileViewController {
                    constructor() {
                        this.CurrentProfile = $("#CurrentProfileId");
                        this.ProfileName = $("#ProfileName");
                        this.LikeCount = $("#LikeCount");
                        this.CityCountryLabel = $("#CityAndCountryLabel");
                        this.ViewingProfileId = $("#ViewingProfileId");
                        this.SendRelateRequestButton = $("#SendRelateRequest");
                        this.SendCommentButton = $("#SendCommentButton");
                        this._service = new Reolin.Web.UI.Services.ProfileService();
                    }
                    static Start() {
                        console.log("in view controller");
                        var controller = new ProfileViewController();
                        controller.Initialize();
                    }
                    Initialize() {
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
                    SetRouteButton() {
                        var button = $("#routeButton");
                        var setSourceHandler = new Net.HttpServiceHandler();
                        setSourceHandler.HandleError = r => console.log(r);
                        setSourceHandler.HandleResponse = r => {
                            var source = r.ResponseBody;
                            button.attr('src-lat', source.latitude);
                            button.attr('src-long', source.longitude);
                        };
                        this._service.GetLocation(this.CurrentProfile.val(), setSourceHandler);
                        var setDestinationHandler = new Net.HttpServiceHandler();
                        setDestinationHandler.HandleError = r => console.log(r);
                        setDestinationHandler.HandleResponse = r => {
                            var dest = r.ResponseBody;
                            button.attr('dest-lat', dest.latitude);
                            button.attr('dest-long', dest.longitude);
                        };
                        this._service.GetLocation(this.ViewingProfileId.val(), setDestinationHandler);
                        button.click(e => {
                            window.location.href =
                                '/'
                                    + button.attr('src-lat')
                                    + '/and/'
                                    + button.attr('src-long')
                                    + '/to/' + button.attr('dest-lat')
                                    + '/and/' + button.attr('dest-long');
                        });
                    }
                    SendCommentButton_ClickHandler(e) {
                        var data = {
                            ProfileId: this.ViewingProfileId.val(),
                            Message: $("#CommentText").val()
                        };
                        console.log(data);
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = r => console.log(r);
                        handler.HandleResponse = r => alert('comment sent, and will be visible after has been approved');
                        this._service.AddComment(data, handler);
                    }
                    SetCertificates() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in SetCerticates" + r.Error);
                        handler.HandleResponse = (r) => {
                            var markUp = $("#certificateItemTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#certificateList");
                        };
                        this._service.GetCertificates(this.ViewingProfileId.val(), handler);
                    }
                    SetRelations() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in " + r.Error);
                        handler.HandleResponse = (r) => {
                            var acceptedList = r.ResponseBody.filter(e => e.confirmed == true);
                            var markUp = $("#relatedTemplate");
                            renderEngine.tmpl(markUp, acceptedList).appendTo("#relationsList");
                            //var listMarkUp = $("#relationRequestTemplate");
                            //renderEngine.tmpl(listMarkUp, acceptedList).appendTo("#relationRequestList");
                        };
                        this._service.GetRequestRelatedProfiles(this.ViewingProfileId.val(), handler);
                    }
                    SetImageGalleries() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in " + r.Error);
                        handler.HandleResponse = (r) => {
                            var markup = $("#imagesTemplate");
                            // Compile the markup as a named template
                            renderEngine.template("images", markup);
                            // Render the template with the movies data
                            renderEngine.tmpl("images", r.ResponseBody, {
                                Host: UrlSource.Host,
                            }).appendTo("#imageList");
                        };
                        this._service.GetImageGalleryItems(this.ViewingProfileId.val(), handler);
                    }
                    SetImageCategories() {
                        var id = this.ViewingProfileId.val();
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log(r.Error);
                        handler.HandleResponse = (r) => {
                            var markUp = $("#imageCategoryTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#imageCategoryList");
                        };
                        this._service.GetImageCategories(id, handler);
                    }
                    SendRelateRequestButton_clickHandler(e) {
                        //Task < int > AddRelate(int sourceId, int targetId, DateTime date, string description, int relatedTypeId);
                        var relatedTypeId = $("#type").val();
                        var sourceId = parseInt($("#CurrentProfileId").val());
                        var targetId = parseInt($("#ViewingProfileId").val());
                        var date = $("#datePicker").val();
                        var description = $("#explain").val();
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleResponse = (r) => alert("done");
                        handler.HandleError = (r) => console.log(r);
                        this._service.SendRelateRequest({
                            relatedTypeId: relatedTypeId,
                            sourceId: sourceId,
                            targetId: targetId,
                            date: date,
                            description: description
                        }, handler);
                    }
                    SetInfoBox() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController");
                        handler.HandleResponse = (r) => {
                            var result = r.ResponseBody;
                            this.CityCountryLabel.text(result.city + "/ " + result.country);
                            this.LikeCount.text(result.likeCount);
                            this.ProfileName.text(result.name);
                        };
                        this._service.GetBasicInfo(this.ViewingProfileId.val(), handler);
                    }
                    SetTags() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController -- set tags");
                        handler.HandleResponse = (r) => {
                            var markUp = $("#secondTagTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#secondTagList");
                        };
                        this._service.GetTags(this.ViewingProfileId.val(), handler);
                    }
                    SetPhoneNumber() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController -- set phoneNumber");
                        handler.HandleResponse = (r) => {
                            var markUp = $("#phoneNumberTemplateContainer");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#phoneNumberList");
                        };
                        this._service.GetPhoneNumber(this.ViewingProfileId.val(), handler);
                    }
                    SetRelatedTypes() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController -- set relatedtype");
                        handler.HandleResponse = (r) => {
                            var markUp = $("#relatedTypeTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#type");
                        };
                        this._service.GetRelatedType(this.ViewingProfileId.val(), handler);
                    }
                }
                Controllers.ProfileViewController = ProfileViewController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
Reolin.Web.Client.Controllers.ProfileViewController.Start();
