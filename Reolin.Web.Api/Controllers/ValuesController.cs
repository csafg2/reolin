using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Reolin.Web.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ILogger<ValuesController> _logger;
        
        public ValuesController(ILogger<ValuesController> logger)
        {
            this._logger = logger;
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Json( new string[] { "value1", "value2" });
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new System.Exception();
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
