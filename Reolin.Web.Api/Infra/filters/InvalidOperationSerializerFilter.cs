#pragma warning disable CS1591
using Microsoft.AspNetCore.Mvc.Filters;
using Reolin.Web.Api.Infra.mvc;
using System;
using System.Net;

namespace Reolin.Web.Api.Infra.filters
{
    public class InvalidOperationSerializerFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidOperationException)
            {
                context.Result = new ApiResult(HttpStatusCode.BadRequest, new
                {
                    Message = context.Exception.Message
                });
            }
        }
    }
}
