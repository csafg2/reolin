using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Web.Api.Infra.mvc;
using Reolin.Web.ViewModels.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Reolin.Web.Api.Controllers
{
    [EnableCors("AllowAll")]
    public class CommentController : BaseController
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
        /// دریافت کامنت های تایید نشده
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        public ActionResult GetUnconfirmedComments(int profileId)
        {
            var comments = _context
               .Comments
               .Where(c => c.ProfileId == profileId)
               .Select(c => new
               {
                   Comment = c,
                   SenderName = c.User.UserName,
                   IconUrl = c.Profile.IconUrl
               });

            return Ok(comments);
        }

        /// <summary>
        /// دریافت کامنت های تایید نشده
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("[controller]/[action]")]
        public ActionResult GetComments(int profileId)
        {
            var comments = _context
                .Comments
                .Where(c => c.ProfileId == profileId && c.Confirmed == true)
                .Select(c => new
                {
                    Comment = c,
                    SenderName = c.User.UserName,
                    IconUrl = c.Profile.IconUrl
                });

            return Ok(comments);
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
            if (comment == null)
            {
                return NotFound();
            }

            comment.Confirmed = true;
            await this._context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Move Comment to history tab
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> LogicalDelete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            comment.IsHistory = true;
            await _context.SaveChangesAsync();

            return Ok();
        }

        /// <summary>
        /// Physically remove the comment from
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("[controller]/[action]")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> PhysicalDelete(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
