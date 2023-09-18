using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MVC.Controllers
{

	public class BlogsController : Controller
    {
        // Add service injections here
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly ITagService _tagService;

        public BlogsController(IBlogService blogService, IUserService userService, ITagService tagService)
        {
            _blogService = blogService;
            _userService = userService;
            _tagService = tagService;
        }

        // GET: Blogs

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated) //Giriş yapıldı mı?
            {
                List<BlogModel> blogList = _blogService.Query().ToList();
                return View(blogList);
            }
            return RedirectToAction("Login", "Users", new { Area = "Account" });
        }

        // GET: Blogs/Details/5
        public IActionResult Details(int id)
        {
            BlogModel blog = _blogService.Query().SingleOrDefault(b => b.Id == id);

            if (blog == null)
            {
                return View("_Error", "Blogs not found!");
            }
            return View(blog);
        }

        // GET: Blogs/Create
        [Authorize]
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            //ViewData["UserId"] = new SelectList(_userService.GetList(), "Id", "UserName");

            ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name");

            BlogModel model = new BlogModel()
            {
                UserId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value)
            };

            return View(model);

        }
    


        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogModel blog)
        {

			if (ModelState.IsValid)
            {
                blog.UserId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
                var result = _blogService.Add(blog);
                
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;

                    return RedirectToAction(nameof(Index));
				}

                //ViewData["Message"] = result.Message; 1. yöntem
                ModelState.AddModelError("", result.Message);
            }
			// Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
			ViewData["UserId"] = new SelectList(_userService.GetList(), "Id", "UserName");
			ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name");
			return View(blog);
        }

        // GET: Blogs/Edit/5
        [Authorize]
        public IActionResult Edit(int id)
        {

            BlogModel blog = _blogService.Query().SingleOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return View("_Error", "Blog not found!");
            }

			var _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
			if (User.IsInRole("User") && _userId != blog.UserId) //Sadece kendi blogunu düzenleyebilecek
            {
				TempData["Message"] = "You don't have permission to edip this blog!";
				return RedirectToAction(nameof(Index));
			}

            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["UserId"] = new SelectList(_userService.GetList(), "Id", "UserName");
            ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name",blog.TagIds);
            return View(blog);
        }

        // POST: Blogs/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BlogModel blog)
        {
			var _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
			
            if (User.IsInRole("User") && _userId != blog.UserId) //Sadece kendi blogunu düzenleyebilecek
			{
				return RedirectToAction(nameof(Index));
			}

			if (ModelState.IsValid)
            {

                if (User.IsInRole("User"))
                {
                    blog.UserId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
				}
                var result = _blogService.Update(blog);
                
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", result.Message);
            }
			ViewData["UserId"] = new SelectList(_userService.GetList(), "Id", "UserName");
			ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name", blog.TagIds);
			return View(blog);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var blog = _blogService.Query().SingleOrDefault(b => b.Id == id);
			
            var _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);
			
            if (User.IsInRole("User") && blog.UserId != _userId)
            {
				TempData["Message"] = "You don't have permission to delete this blog!";
				return RedirectToAction(nameof(Index));
			}
			
            var result = _blogService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
