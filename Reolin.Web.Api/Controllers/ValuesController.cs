using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.mvc;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using System.Collections.Generic;

namespace Reolin.Web.Api.Controllers
{
    public class ValuesController : BaseController
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger, IServiceProvider provider)
        {
            this._logger = logger;
        }

        [HttpGet]
        public string Get2()
        {

            return "HELLOW WORLD!!";
        }

        [Authorize]
        public string Get()
        {

            return "HELLOW WORLD!!";
        }

        [ProducesResponseType(200, StatusCode = 200, Type = typeof(IEnumerable<Data>))]
        [Route("[controller]/[action]")]
        [HttpGet]
        public IActionResult GetData()
        {
            return Json(new[] { new Data() { Value = 1 }, new Data { Value = 45 } });
        }
    }

    public class Data
    {
        public int Value { get; set; }
    }
}

