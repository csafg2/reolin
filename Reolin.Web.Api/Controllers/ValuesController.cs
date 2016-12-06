﻿using System;
using Microsoft.AspNetCore.Mvc;

namespace Reolin.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IServiceProvider _provider;

        public ValuesController(IServiceProvider provider)
        {
            _provider = provider;
        }
        //[Authorize]
        //[HttpGet]
        public IActionResult Get()
        {
            //return Redirect("http://www.google.com");
            return Json( new string[] { "value1", "value2" });
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
