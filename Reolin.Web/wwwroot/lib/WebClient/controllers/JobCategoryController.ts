module Reolin.Web.Client.Controllers
{
    export class JobCategoryController
    {
        public GetJobCategories(dropDown: JQuery)
        {
            var me = this;
            $.getJSON(UrlSource.GetJobCategories, null, function (d)
            {
                for (var categoryItem in d)
                {
                    var item = { name: d[categoryItem].name, id: d[categoryItem].id };
                    dropDown.append("<option value=" + item.id + ">" + item.name + "</option>")
                }
            });
        }

        public GetSubCategories(dropDown: JQuery)
        {
            var me = this;

            $.getJSON(UrlSource.GetSubJobCategories, null, function (d)
            {
                for (var c in d)
                {
                    var item = { name: d[c].name, id: d[c].id };

                    dropDown.append("<option value=" + item.id + ">" + item.name + "</option>");
                }
            });
        }
    }
}