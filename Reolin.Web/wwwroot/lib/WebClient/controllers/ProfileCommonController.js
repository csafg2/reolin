var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                class ProfileCommonController {
                    constructor() {
                        this._service = new Reolin.Web.UI.Services.ProfileService();
                        this.ViewingProfileId = $("#ViewingProfileId"); //.val();
                        this.SetComments();
                        this.SetTags();
                    }
                    SetComments() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController -- LatestComments");
                        handler.HandleResponse = (r) => {
                            var markUp = $("#commentTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#commentList");
                        };
                        this._service.GetLatestComments(this.ViewingProfileId.val(), handler);
                    }
                    SetTags() {
                        var handler = new Net.HttpServiceHandler();
                        handler.HandleError = (r) => console.log("error in ProfileViewController -- set tags");
                        handler.HandleResponse = (r) => {
                            var markUp = $("#tagTemplate");
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#tagsList");
                        };
                        this._service.GetTags(this.ViewingProfileId.val(), handler);
                    }
                    static Start() {
                        var controller = new ProfileCommonController();
                    }
                }
                Controllers.ProfileCommonController = ProfileCommonController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
Reolin.Web.Client.Controllers.ProfileCommonController.Start();
