#pragma warning disable CS1591
using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Username is required", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}
