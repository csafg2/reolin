using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reolin.Data;
using Reolin.Data.Domain;
using Reolin.Web.Api.Infra.mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Reolin.Web.Api.Controllers
{
    public class CreateCommentModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Comment body is required")]
        public string Body { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "invalid profile id")]
        public int ProfileId { get; set; }
    }

    public class CommentReplyModel: CreateCommentModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "invalid comment id")]
        public int CommentId { get; set; }
    }

    public class CommentController: BaseController
    {
        DataContext context = new DataContext();

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
            context.Comments.Add(new Comment()
            {
                Body = model.Body,
                Date = DateTime.Now,
                Confirmed = false,
                ProfileId = model.ProfileId,
                UserId = this.GetUserId()
            });

            context.SaveChanges();

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
            context.Comments.Add(new Comment()
            {
                Body = model.Body,
                Date = DateTime.Now,
                Confirmed = false,
                ProfileId = model.ProfileId,
                UserId = this.GetUserId(),
                ParentId = model.CommentId
            }); 


            context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// دریافت پاسخ های یک کامنت
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public ActionResult GetReplies(int commentId)
        {
            return Ok(context.Comments.Where(c => c.ParentId == commentId));
        }

        /// <summary>
        /// دریافت کامنت های یک پروفایل
        /// </summary>
        /// <param name="profileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[controller]/[action]")]
        public ActionResult GetComments(int profileId)
        {
            return Ok(context.Comments.Where(c => c.ProfileId == profileId));
        }
    }
}
