using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reolin.Web.Api.Infra.mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Reolin.Web.Api.Infra.IO;

namespace Reolin.Web.Api.Controllers
{
    public class FileController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            
            var file = Request.Form.Files[0];
            var service = new FileService(@"E:\data", new DirectoryProvider());
            using (var stream = file.OpenReadStream())
            {
                stream.Position = 0;
                string path = await service.SaveAsync(stream, file.FileName);
                return Ok(path);
            }
        }
    }
}
