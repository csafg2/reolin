

module Reolin.Web.Client.Controllers
{
    declare var renderEngine;

    export class ProfileCommonController
    {
        protected _service: Reolin.Web.UI.Services.ProfileService = new Reolin.Web.UI.Services.ProfileService();
        protected ViewingProfileId: JQuery = $("#ViewingProfileId");//.val();

        constructor()
        {
            this.SetComments();
            this.SetTags();
        }

        private SetComments(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController -- LatestComments");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var markUp = $("#commentTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#commentList");
            };

            this._service.GetLatestComments(this.ViewingProfileId.val(), handler);
        }

        private SetTags(): void
        {
            var handler = new Net.HttpServiceHandler();
            handler.HandleError = (r: HttpResponse) => console.log("error in ProfileViewController -- set tags");
            handler.HandleResponse = (r: HttpResponse) =>
            {
                var markUp = $("#tagTemplate");
                renderEngine.tmpl(markUp, r.ResponseBody).appendTo("#tagsList");
            };

            this._service.GetTags(this.ViewingProfileId.val(), handler);

        }

        public static Start(): void
        {
            var controller = new ProfileCommonController();
        }
    }
}

Reolin.Web.Client.Controllers.ProfileCommonController.Start();