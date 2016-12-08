using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.mvc
{

    public class ApiResult : ActionResult
    {
        private object _data;
        private HttpStatusCode _statusCode;

        public ApiResult(HttpStatusCode statusCode, object data)
        {
            this._statusCode = statusCode;
            this._data = data;
        }

        public async override void ExecuteResult(ActionContext context)
        {
            await this.ExecuteResultAsync(context);
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)this._statusCode;
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_data));
            return response.Body.WriteAsync(body, 0, body.Length);

        }
    }

}
