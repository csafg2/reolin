var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                class ChooseProfileController {
                    static Start() {
                        var service = new Reolin.Web.Client.HttpService();
                        var userId = parseInt(store.Get().GetKey("Id"));
                        console.log(userId);
                        var handler = new Client.HttpServiceHandler();
                        handler.HandleResponse = (r) => {
                            for (var p in r.ResponseBody) {
                                var body = r.ResponseBody;
                                var link = '<a href="/User/SetActive/' + body[p].id + '">' + body[p].name + '</a>';
                                console.log(link);
                                ChooseProfileController._list.append('<li>' + link + '</li>');
                            }
                        };
                        handler.HandleError = (r) => { alert('error'); };
                        service.GetWithData(UrlSource.QueryUserProfiles, {}, 2, true, { id: userId }, handler);
                    }
                }
                ChooseProfileController._list = $("#userProfilesList");
                Controllers.ChooseProfileController = ChooseProfileController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
Reolin.Web.Client.Controllers.ChooseProfileController.Start();
