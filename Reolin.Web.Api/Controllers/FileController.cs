using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.IO;
using Reolin.Web.Api.Infra.mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]

    public class FileController : BaseController
    {
        private IFileService _service;

        public FileController(IFileService fileService)
        {
            this._service = fileService;
        }

        /// <summary>
        /// ارسال فایل
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var file = Request.Form.Files[0];
            using (var stream = file.OpenReadStream())
            {
                stream.Position = 0;
                string path = await _service.SaveAsync(stream, file.FileName);
                return Ok(path);
            }
        }
    }
}
