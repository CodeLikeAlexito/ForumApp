using Forum.Areas.Identity.Data;
using Forum.Models;
using Forum.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System;
using Microsoft.EntityFrameworkCore;
using static Forum.Helper;
using Forum.ViewModels;

namespace Forum.Controllers
{
    [Authorize]
    public class TopicController : Controller
    {

        private readonly IGenericRepository<Topic> _topicRepo;
        private readonly IGenericRepository<Comment> _commentRepo;
        private readonly IGenericRepository<Replay> _replayRepo;
        private readonly UserManager<ForumUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public TopicController(IGenericRepository<Topic> topicRepo, UserManager<ForumUser> userManager, IGenericRepository<Comment> commentRepo,
                                    IGenericRepository<Replay> replayRepo, IWebHostEnvironment hostEnvironment)
        {
            _topicRepo = topicRepo;
            _commentRepo = commentRepo;
            _replayRepo = replayRepo;
            _userManager = userManager;
            webHostEnvironment = hostEnvironment;
        }

        /*
        [HttpGet]
        public async Task<IActionResult> Index(string searchQuery)
        {
            ForumViewModel model = null;// = new ForumViewModel();


            if (String.IsNullOrEmpty(searchQuery))
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
        }*/

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ForumViewModel model = null;
            model = new ForumViewModel()
            {
                Topics = await _topicRepo.GetAllAsync(),
                Comments = await _commentRepo.GetAllAsync(),
                Replays = await _replayRepo.GetAllAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string searchQuery)
        {
            ForumViewModel model = null;// = new ForumViewModel();


            if (String.IsNullOrEmpty(searchQuery))
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

        /*
        [HttpGet("topic")]
        public async Task<IActionResult> Index()
        {
            var model = await _topicRepo.GetAllAsync();

            return View(model);
        }

        [HttpGet("topic/{id}")]
        public async Task<ActionResult<Topic>> Index(int id)
        {
            var model = await _topicRepo.GetByIdAsync(id);

            return View(model);
        }
        */

        /*
        [HttpGet]
        public async Task<IEnumerable<Topic>> GetAllTopics()
        {
            if (_topicRepo.GetAllAsync() == null)
                return null;
            return await _topicRepo.GetAllAsync();
        }
        */
        // GET: Topic/AddOrEdit(Insert)
        // GET: Topic/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Topic());
            else
            {
                var topicModel = await _topicRepo.GetByIdAsync(id);
                if (topicModel == null)
                {
                    return NotFound();
                }
                return View(topicModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [NoDirectAccess]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("TopicId,Title,Description,DateCreated,DateModified,UserId,TopicPicture, UserName")] Topic topicModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            ForumUser applicationUser = await _userManager.GetUserAsync(User);
            string userEmail = applicationUser?.Email; // will give the user's Email

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(topicModel);
                //Insert
                if (id == 0)
                {
                    topicModel.DateCreated = DateTime.Now;
                    topicModel.UserId = userId;
                    topicModel.UserName = userName;
                    topicModel.TopicPicture = uniqueFileName;
                    _topicRepo.Insert(topicModel);
                    await _topicRepo.SaveAsync();

                }
                //Update
                else
                {
                    try
                    {
                        _topicRepo.Update(topicModel);
                        await _topicRepo.SaveAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (TopicModelExists(id) == null)
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
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _topicRepo.GetAllAsync()) }) ;
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", topicModel) });
        }

        private async Task<Topic> TopicModelExists(int id)
        {
            return await _topicRepo.GetByIdAsync(id);
        }

        /*
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

            string uniqueFileName = UploadedFile(topic);

            topic.UserId = userId;
            topic.UserName = userName;
            topic.TopicPicture = uniqueFileName;

            if (ModelState.IsValid)
            {
                _topicRepo.Insert(topic);
                _topicRepo.Save();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        */
        private string UploadedFile(Topic topic)
        {
            string uniqueFileName = null;

            if (topic.TopicImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + topic.TopicImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    topic.TopicImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        /*
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
        */
    }
}
