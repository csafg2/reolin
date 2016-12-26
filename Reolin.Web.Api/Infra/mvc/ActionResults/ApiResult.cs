using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Infra.mvc
{
    /// <summary>
    /// a wrapper for for JsonResult to be able to set Http StatusCode.
    /// </summary>
    public class ApiResult : ActionResult
    {
        private readonly object _data;
        private readonly int _statusCode;
        private const string JSON_MEDIA_TYPE = "application/json";

        public ApiResult(object data) : this(HttpStatusCode.Accepted, data)
        {

        }

        public ApiResult(HttpStatusCode statusCode, object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            this._statusCode = (int)statusCode;
            this._data = data;
        }

        public override void ExecuteResult(ActionContext context)
        {
            this.Write(context, this.CreateResponse(context));
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            return this.WriteAsync(context, this.CreateResponse(context));
        }

        private byte[] CreateResponse(ActionContext context)
        {
            HttpResponse response = context.HttpContext.Response;
            response.ContentType = JSON_MEDIA_TYPE;
            response.StatusCode = this._statusCode;
            
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_data));
        }

        private void Write(ActionContext context, byte[] message)
        {
            context.HttpContext.Response.Body.Write(message, 0, message.Length);
        }

        private Task WriteAsync(ActionContext context, byte[] message)
        {
            return context.HttpContext.Response.Body.WriteAsync(message, 0, message.Length);
        }
    }
}
