using Forum.Areas.Identity.Data;
using Forum.Models;
using Forum.Repositories;
using Forum.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Forum.Helper;

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

        /*
        [HttpGet("comment")]
        public ActionResult Index()
        {
            return View();
        }*/

        /*
        [HttpPost]
        public async Task<JsonResult> AddComment([FromBody] Comment comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            ForumUser applicationUser = await _userManager.GetUserAsync(User);
            string userEmail = applicationUser?.Email; // will give the user's Email

            comment.DateCreated = DateTime.Now;
            comment.DateModified = DateTime.Now;
            comment.TopicId = 2;

            if(ModelState.IsValid)
            {
                _commentRepo.Insert(comment);
                _commentRepo.Save();
                return Json("succes");
            }
            
            
            return Json("error");
        }*/

        [NoDirectAccess]
        public async Task<IActionResult> AddComment(int id = 0, int topicId = 0)
        {
            if (topicId != 0)
            {
                if (id == 0)
                    return View(new Comment());
                else
                {
                    var commentModel = await _commentRepo.GetByIdAsync(id);
                    if (commentModel == null)
                    {
                        return NotFound();
                    }
                    return View(commentModel);
                }
            }
            else
                return NotFound();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public async Task<IActionResult> AddComment(int id, [Bind("CommentId,Text,DateCreated,DateModified,UserId,TopicId, UserName")] Comment commentModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ForumUser applicationUser = await _userManager.GetUserAsync(User);
            string userEmail = applicationUser?.Email; // will give the user's Email

            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    commentModel.DateCreated = DateTime.Now;
                    commentModel.UserId = userId;
                    commentModel.UserName = userName;
                    //commentModel.TopicId = topicId;
                    _commentRepo.Insert(commentModel);
                    await _commentRepo.SaveAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _commentRepo.Update(commentModel);
                        await _commentRepo.SaveAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (CommentModelExists(id) == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                //var res = _topicRepo.GetByIdAsync(id);
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _commentRepo.GetAllAsync()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddComment", commentModel) });
        }

        private async Task<Comment> CommentModelExists(int id)
        {
            return await _commentRepo.GetByIdAsync(id);
        }

    }
}
