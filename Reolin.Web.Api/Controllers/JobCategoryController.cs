﻿using Microsoft.AspNetCore.Mvc;
using Reolin.Data.Services.Core;
using Reolin.Web.Api.Infra.mvc;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
#pragma warning disable CS1591
    public class JobCategoryController: BaseController
    {
        private IJobCategoryService _service;

        public JobCategoryController(IJobCategoryService service)
        {
            this._service = service;
        }

        private IJobCategoryService Service
        {
            get
            {
                return _service;
            }
        }

#pragma warning restore CS1591


        /// <summary>
        /// return a list of available job categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> GetAll()
        {
            var data = await this.Service.GetAllAsync();

            return Ok(data);
        }
    }
}
