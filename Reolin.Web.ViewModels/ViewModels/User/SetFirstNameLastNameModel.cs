﻿#pragma warning disable CS1591
using System.ComponentModel.DataAnnotations;

namespace Reolin.Web.ViewModels
{

    public class SetFirstNameLastNameModel
    {
        [Required(ErrorMessage = "Firstname is required", AllowEmptyStrings = false)]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Lastname is required", AllowEmptyStrings = false)]
        [MaxLength(40)]
        public string LastName { get; set; }

    }
}
