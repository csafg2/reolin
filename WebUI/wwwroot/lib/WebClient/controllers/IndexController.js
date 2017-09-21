var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                var Services = Reolin.Web.UI.Services;
                class IndexController {
                    constructor() {
                        this._mainSearchBox = $("#mainserach");
                        this._profileService = new Services.ProfileService();
                        this._currentSubCatId = -1;
                        this._searchProfilehandler = new Client.HttpServiceHandler();
                        var me = this;
                        this._mainSearchBox.bind("enterKey", function (e) {
                            var textInput = me._mainSearchBox.val().split("|");
                            var searchTerm = (textInput.length > 1 ? textInput[1] : textInput[0]).replace(" ", "");
                            console.log(searchTerm);
                            if (IsNullOrEmpty(searchTerm) || me._currentSubCatId == -1) {
                                return;
                            }
                            me.Search(searchTerm);
                        });
                        this._mainSearchBox.keyup(function (e) {
                            if (e.keyCode == 13) {
                                $(this).trigger("enterKey");
                            }
                        });
                    }
                    Search(searchTerm) {
                        var me = this;
                        var model = new Services.SearchProfileModel();
                        model.Distance = 60000;
                        model.JobCategoryId = me._currentMainCatId;
                        model.SubJobCategoryId = me._currentSubCatId;
                        model.SearchTerm = searchTerm;
                        model.SourceLatitude = currentPos.lat;
                        model.SourceLongitude = currentPos.lng;
                        me._profileService.Find(model, me._searchProfilehandler);
                    }
                    static Start() {
                        var controller = new IndexController();
                        controller.Init();
                    }
                    Init() {
                        var me = this;
                        $("#catMenu").on('click', '.catItem', function () {
                            me._currentMainCatId = parseInt($(this).attr("data-catId"));
                        });
                        $("#subCatMenu").on('click', '.subCatItem', function () {
                            me._currentSubCatId = parseInt($(this).attr("data-subCatId"));
                            $(".active-main-cat").removeClass("active-main-cat");
                            $(this).addClass("active-main-cat");
                        });
                        var controller = new Reolin.Web.Client.Controllers.JobCategoryController();
                        controller.GetSubCategoriesList(function (d) {
                            var menu = $("#subCatMenu");
                            for (var p in d) {
                                var item = { name: d[p].name, id: d[p].id };
                                var html = '<li>'
                                    + '<a href="#" data-search="Manufacture" class="subCatItem" data-subCatId="'
                                    + item.id
                                    + '">'
                                    + item.name + '</a>'
                                    + '</li>';
                                menu.append(html);
                            }
                            menu.children(":first-child").addClass("active-main-cat");
                            me._currentSubCatId = d[0].id;
                        });
                        controller.GetJobCategorieList(function (d) {
                            var menu = $("#catMenu");
                            for (var p in d) {
                                var item = { name: d[p].name, id: d[p].id };
                                var html = '<li class="select-cat">' +
                                    '<a href="#" class="catItem" click="()" data-catId="' + item.id + '">' + item.name + '</a></li>';
                                menu.append(html);
                            }
                        });
                        this._searchProfilehandler.HandleResponse = (r) => {
                            var markUp = $("#Distance");
                            $("#profileList").html('');
                            renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#profileList");
                        };
                        this._searchProfilehandler.HandleError = (r) => console.log(r);
                    }
                }
                Controllers.IndexController = IndexController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
