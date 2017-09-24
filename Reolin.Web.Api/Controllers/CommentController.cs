using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Web.Api.Infra.mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using Reolin.Web.ViewModels.ViewModels;

namespace Reolin.Web.Api.Controllers
{
    public class CommentController: BaseController
    {
        private readonly DataContext _context;

        public CommentController(DataContext context)
        {
            this._context = context;
        }
        /// <summary>
        /// ارسال کامنت جدید
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public ActionResult Create(CreateCommentModel model)
        {
            _context.Comments.Add(new Comment()
            {
                Body = model.Body,
                Date = DateTime.Now,
                Confirmed = false,
                ProfileId = model.ProfileId,
                UserId = this.GetUserId()
            });

            _context.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// پاسخ به کامنت
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public ActionResult Reply(CommentReplyModel model)
        {
            _context.Comments.Add(new Comment()
            {
                Body = model.Body,
                Date = DateTime.Now,
                Confirmed = false,
                ProfileId = model.ProfileId,
                UserId = this.GetUserId(),
                ParentId = model.CommentId
            });


            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// دریافت پاسخ های یک کامنت
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public ActionResult GetReplies(int commentId)
        {
            return Ok(_context.Comments.Where(c => c.ParentId == commentId));
        }

        /// <summary>
        /// دریافت کامنت های یک پروفایل
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public ActionResult GetComments(int profileId)
        {
            return Ok(_context.Comments.Where(c => c.ProfileId == profileId && c.Confirmed == true));
        }

        /// <summary>
        /// تایید کامنت
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> Confirm(int commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if(comment == null)
            {
                return NotFound();
            }

            comment.Confirmed = true;
            await this._context.SaveChangesAsync();
            return Ok();
        }
    }
}
