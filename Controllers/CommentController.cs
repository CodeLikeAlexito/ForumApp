using Forum.Areas.Identity.Data;
using Forum.Models;
using Forum.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    public class CommentController : Controller
    {
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly UserManager<ForumUser> _userManager;

        public CommentController(IGenericRepository<Comment> commentRepo, UserManager<ForumUser> userManager)
        {
            _commentRepo = commentRepo;
            _userManager = userManager;
        }

        [HttpPost]
        public JsonResult AddComment(Comment comment)
        {
            comment = new Comment()
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Text = comment.Text
            };
            //comment.DateCreated = DateTime.Now;
            //comment.DateModified = DateTime.Now;
            _commentRepo.Insert(comment);
            return Json(comment);
        }

    }
}
