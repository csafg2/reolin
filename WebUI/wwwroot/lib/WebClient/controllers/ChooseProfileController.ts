
module Reolin.Web.Client.Controllers
{
    export class ChooseProfileController
    {
        static _list: JQuery = $("#userProfilesList");

        public static Start()
        {
            var service: Reolin.Web.Client.HttpService = new Reolin.Web.Client.HttpService();
            var userId = parseInt(store.Get().GetKey("Id"));
            console.log(userId);
            var handler = new HttpServiceHandler();
            handler.HandleResponse = (r: HttpResponse) =>
            {
                for (var p in r.ResponseBody)
                {
                    var body = r.ResponseBody;
                    var link = '<a href="/User/SetActive/' + body[p].id + '">' + body[p].name + '</a>';
                    console.log(link);
                    ChooseProfileController._list.append('<li>' + link + '</li>');
                }
            };

            handler.HandleError = (r: HttpResponse) => { alert('error'); };
            service.GetWithData(UrlSource.QueryUserProfiles, {}, 2, true, { id: userId }, handler);

        }
    }
}

Reolin.Web.Client.Controllers.ChooseProfileController.Start();