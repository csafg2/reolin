var Reolin;
(function (Reolin) {
    var Web;
    (function (Web) {
        var Client;
        (function (Client) {
            var Controllers;
            (function (Controllers) {
                class JobCategoryController {
                    GetJobCategorieList(callBack) {
                        $.getJSON(UrlSource.GetJobCategories, null, callBack);
                    }
                    GetJobCategories(dropDown) {
                        var me = this;
                        $.getJSON(UrlSource.GetJobCategories, null, function (d) {
                            for (var categoryItem in d) {
                                var item = { name: d[categoryItem].name, id: d[categoryItem].id };
                                dropDown.append("<option value=" + item.id + ">" + item.name + "</option>");
                            }
                        });
                    }
                    GetSubCategories(dropDown) {
                        var me = this;
                        $.getJSON(UrlSource.GetSubJobCategories, null, function (d) {
                            for (var c in d) {
                                var item = { name: d[c].name, id: d[c].id };
                                dropDown.append("<option value=" + item.id + ">" + item.name + "</option>");
                            }
                        });
                    }
                    GetSubCategoriesList(callBack) {
                        $.getJSON(UrlSource.GetSubJobCategories, null, callBack);
                    }
                }
                Controllers.JobCategoryController = JobCategoryController;
            })(Controllers = Client.Controllers || (Client.Controllers = {}));
        })(Client = Web.Client || (Web.Client = {}));
    })(Web = Reolin.Web || (Reolin.Web = {}));
})(Reolin || (Reolin = {}));
