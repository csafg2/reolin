using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reolin.Web.Api.Infra.mvc;
using System;
using Reolin.Web.Api.Infra.filters;
using Microsoft.AspNetCore.Authorization;
using Reolin.Web.Security.Jwt;

namespace Reolin.Web.Api.Controllers
{
    public class MyViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    [Route("api/[controller]")]
    public class ValuesController : BaseController
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger, IServiceProvider provider)
        {
            this._logger = logger;
        }

        [Authorize]
        //[HttpGet]
        //[OutputCache(Key = "tag",  AbsoluteExpiration = 3600, SlidingExpiration = 1800)]
        public IActionResult Get(string tag)
        {
            return Json(new { Number = new Random().Next(1, 500) });
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post(string value)
        {
            return Redirect("http://www.google.com");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        
        public void Delete(int id)
        {
        }
    }
}
