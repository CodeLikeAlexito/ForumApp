using Forum.Areas.Identity.Data;
using Forum.Models;
using Forum.Repositories;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGenericRepository<Topic> _topicRepo;
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly IGenericRepository<Replay> _replayRepo;
        private readonly UserManager<ForumUser> _userManager;
        

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Topic> topicRepo, IGenericRepository<Comment> commentRepo,
                                    IGenericRepository<Replay> replayRepo, UserManager<ForumUser> userManager)
        {
            _logger = logger;
            _topicRepo = topicRepo;
            _commentRepo = commentRepo;
            _replayRepo = replayRepo;
            _userManager = userManager;
            
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            ForumViewModel model = null;// = new ForumViewModel();

            
            if(String.IsNullOrEmpty(searchQuery))
            {
                model = new ForumViewModel()
                {
                    Topics = await _topicRepo.GetAllAsync(),
                    Comments = await _commentRepo.GetAllAsync(),
                    Replays = await _replayRepo.GetAllAsync()
                };
            }
            else
            {

                model = new ForumViewModel()
                {
                    //Topics = await _topicRepo.GetFilteredDataAsync(searchQuery),

                    //Topics =  model.Topics.Where(t => t.Title.Contains(searchQuery)),
                    Topics = _topicRepo.QueriedTopics(searchQuery),
                    Comments = await _commentRepo.GetAllAsync(),
                    Replays = await _replayRepo.GetAllAsync()
                };
            }

            return View(model);
        }

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

            if (ModelState.IsValid)
            {
                _commentRepo.Insert(comment);
                _commentRepo.Save();
                return Json("succes");
            }


            return Json("error");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Search(string searchQuery)
        {
            return View();
        }
    }
}
