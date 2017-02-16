
module Reolin.Web.Client.Controllers
{
    declare var currentPos;
    declare var renderEngine;

    import Services = Reolin.Web.UI.Services;
    export class IndexController
    {
        private _mainSearchBox: JQuery = $("#mainserach");
        private _profileService: Services.ProfileService = new Services.ProfileService();
        private _currentMainCatId: number;
        private _currentSubCatId: number = -1;
        private _searchProfilehandler: HttpServiceHandler = new HttpServiceHandler();
        
        constructor()
        {
            var me = this;
            this._mainSearchBox.bind("enterKey", function (e)
            {
                var textInput = me._mainSearchBox.val().split("|");
                
                var searchTerm: string = (textInput.length > 1 ? textInput[1] : textInput[0]).replace(" ", "");
                console.log(searchTerm);
                if (IsNullOrEmpty(searchTerm) || me._currentSubCatId == -1)
                {
                    return;
                }
                
                me.Search(searchTerm);
            });

            this._mainSearchBox.keyup(function (e)
            {
                if (e.keyCode == 13)
                {
                    $(this).trigger("enterKey");
                }
            });
        }

        public Search(searchTerm: string): void 
        {
            var me = this;
            var model = new Services.SearchProfileModel();
            model.Distance = 20000;
            model.JobCategoryId = me._currentMainCatId;
            model.SubJobCategoryId = me._currentSubCatId;
            model.SearchTerm = searchTerm;
            //model.SourceLatitude = currentPos.lat;
            //model.SourceLongitude = currentPos.lng;
            model.SourceLatitude = 87;
            model.SourceLongitude = 84;

            me._profileService.Find(model, me._searchProfilehandler);
        }

        public static Start(): void 
        {
            var controller: IndexController = new IndexController();
            controller.Init();
        }

        private Init(): void 
        {
            var me = this;
            $("#catMenu").on('click', '.catItem', function ()
            {
                me._currentMainCatId = parseInt($(this).attr("data-catId"));
            });

            $("#subCatMenu").on('click', '.subCatItem', function ()
            {
                me._currentSubCatId = parseInt($(this).attr("data-subCatId"));
                $(".active-main-cat").removeClass("active-main-cat");
                $(this).addClass("active-main-cat");
            });

            var controller = new Reolin.Web.Client.Controllers.JobCategoryController();

            controller.GetSubCategoriesList(function (d)
            {
                var menu = $("#subCatMenu");
                for (var p in d)
                {
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
            });
            controller.GetJobCategorieList(function (d)
            {
                var menu = $("#catMenu");
                for (var p in d)
                {
                    var item = { name: d[p].name, id: d[p].id };
                    var html = '<li class="select-cat">' +
                        '<a href="#" class="catItem" click="()" data-catId="' + item.id + '">' + item.name + '</a></li>';
                    menu.append(html);
                }
            });

            this._searchProfilehandler.HandleResponse = (r: HttpResponse): void =>
            {
                var markUp = $("#Distance");
                $("#profileList").html('');
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#profileList");
            };
            this._searchProfilehandler.HandleError = (r: HttpResponse): void => console.log(r);

        }
    }
}
