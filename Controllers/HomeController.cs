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

        public IActionResult Index()
        {
            var model = new ForumViewModel();
            model.Topics = _topicRepo.GetAll();
            model.Comments = _commentRepo.GetAll();
            model.Replays = _replayRepo.GetAll();
            return View(model);
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
    }
}
