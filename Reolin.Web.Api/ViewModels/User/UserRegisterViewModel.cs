#pragma warning disable CS1591 
using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.Api.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        [MaxLength(80, ErrorMessage = "Username can not be more than 80 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [MaxLength(50, ErrorMessage = "Password can not be more than 50 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required", AllowEmptyStrings = false)]
        [Compare(otherProperty: "ConfirmPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [RegularExpression(pattern: @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email format is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is requried")]
        public string PhoneNumber { get; set; }
    }

}
