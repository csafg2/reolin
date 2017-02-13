﻿#pragma warning disable CS1591

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Reolin.Web.Api.Infra.Filters;
using Reolin.Web.Api.Infra.filters;

namespace Reolin.Web.Api.Controllers
{
    public class SampleModel
    {
        public int ProfileId { get; set; }
    }

    [InvalidOperationSerializerFilter]
    [EnableCors("AllowAll")]
    public class ValuesController : BaseController
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger, IServiceProvider provider)
        {
            this._logger = logger;
        }

        [HttpPost]
        [CheckPermission]
        public string Get2(SampleModel model)
        {

            return "HELLOW WORLD!!";
        }

        [Authorize]
        [HttpGet]
        [Route("[controller]/[action]")]
        public string Get()
        {

            return "HELLOW WORLD!!";
        }
        
        [HttpGet]
        public JsonResult Get3()
        {
            throw new InvalidOperationException("Some eception !");
            return Json(new { Name = "Hassan" });
        }
    }
}