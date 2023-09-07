#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Services;
using Business.Models;

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
            List<BlogModel> blogList = _blogService.Query().ToList();
            return View(blogList);
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
        public IActionResult Create()
        {
            // Add get related items service logic here to set ViewData if necessary and update null parameter in SelectList with these items
            ViewData["UserId"] = new SelectList(_userService.GetList(), "Id", "UserName");
            ViewBag.Tags = new MultiSelectList(_tagService.Query().ToList(), "Id", "Name");
            return View();
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
        public IActionResult Edit(int id)
        {
            BlogModel blog = _blogService.Query().SingleOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return View("_Error","Blog not found!");
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
            if (ModelState.IsValid)
            {

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


        public IActionResult Delete(int id)
        {

            var result = _blogService.Delete(id);

            TempData["Message"] = result.Message;

            return RedirectToAction(nameof(Index));
        }
	}
}
