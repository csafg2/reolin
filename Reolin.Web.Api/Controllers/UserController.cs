using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Services;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    public class UserController: BaseController
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            this._service = service;
        }

        public IUserService UserService
        {
            get
            {
                return _service;
            }
        }


        // TODO: complete this method
        public IActionResult SetFirstNameLastName(SetFirstNameLastNameModel model)
        {
            throw new NotImplementedException();
            if(!this.ModelState.IsValid)
            {
                
                return BadRequest(this.ModelState);
            }

            try
            {
                
                //this.UserService.SetUserInfo(User.Claims.)
            }
            catch(Exception ex)
            { }

        }
        
    }
    public class SetFirstNameLastNameModel
    {
        [Required(ErrorMessage = "Firstname is required", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required", AllowEmptyStrings = false)]
        public  string LastName { get; set; }

    }


}
