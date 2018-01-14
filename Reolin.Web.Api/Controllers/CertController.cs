using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class EditCertModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Invalid Desc")]
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid Id")]
        public int Id { get; set; }


        [Range(1950, 2018, ErrorMessage = "Invalid Year must be 1950-2018")]
        public int Year { get; set; }
    }

    [EnableCors("AllowAll")]
    public class CertController : ControllerBase
    {
        private readonly DataContext _context;
        public CertController(DataContext context)
#pragma warning restore CS1591
        {
            this._context = context;
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Edit(EditCertModel model)
        {
            var cer = await this._context.Certificates.FirstOrDefaultAsync(c => c.Id == model.Id);
            cer.Description = model.Description;
            cer.Year = model.Year;

            await this._context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int id)
        {
            var cert = await this._context.Certificates.FirstOrDefaultAsync(c => c.Id == id);
            if (cert == null)
            {
                return NotFound();
            }

            this._context.Certificates.Remove(cert);
            await this._context.SaveChangesAsync();

            return Ok();
        }
    }
}
