using Forum.Areas.Identity.Data;
using Forum.Models;
using Forum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Authorize]
    public class TopicController : Controller
    {

        private readonly IGenericRepository<Topic> _topicRepo;
        private readonly UserManager<ForumUser> _userManager;

        public TopicController(IGenericRepository<Topic> topicRepo, UserManager<ForumUser> userManager)
        {
            this._topicRepo = topicRepo;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _topicRepo.GetAll();

            return View(model);
        }

        [HttpGet]
        public IEnumerable<Topic> GetAllTopics()
        {
            if (_topicRepo.GetAll() == null)
                return null;
            return _topicRepo.GetAll();
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> New(Topic topic) // [FromBody] if i want to test it from POSTMAN
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName

            ForumUser applicationUser = await _userManager.GetUserAsync(User);
            string userEmail = applicationUser?.Email; // will give the user's Email

            topic.UserId = userId;
            topic.UserName = userName;

            if (ModelState.IsValid)
            {
                _topicRepo.Insert(topic);
                _topicRepo.Save();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult New1([FromBody] Topic topic) // [FromBody] if i want to test it from POSTMAN
        {
            if(ModelState.IsValid)
            {
                _topicRepo.Insert(topic);
                _topicRepo.Save();
                return Content("success");
            }
            return Content("Fail");
        }
        
    }
}
