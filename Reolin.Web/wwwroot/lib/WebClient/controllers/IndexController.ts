module Reolin.Web.Client.Controllers
{
    export class IndexController
    {
        private _mainSearchBox: JQuery = $("#mainserach");

        
        constructor()
        {
            this._mainSearchBox.bind("enterKey", function (e)
            {
                alert("Enter");
            });

            this._mainSearchBox.keyup(function (e)
            {
                if (e.keyCode == 13)
                {
                    $(this).trigger("enterKey");
                }
            });
        }

        public static Start(): void 
        {
            var controller: IndexController = new IndexController();
        }
    }
}
