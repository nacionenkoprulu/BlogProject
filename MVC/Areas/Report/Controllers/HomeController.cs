using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Areas.Report.Models;


namespace MVC.Areas.Report.Controllers
{
    [Area("Report")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public HomeController(IReportService reportService, IUserService userService, IRoleService roleService)
        {
            _reportService = reportService;
            _userService = userService;
            _roleService = roleService;
        }



        // GET: HomeController
        public ActionResult Index()
        {

            var model = _reportService.GetList(false); //false gönderip left join alıyoruz

            var viewModel = new HomeIndexViewModel()
            {
                Reports = model,
                UserSelectList = new SelectList(_userService.GetListByRole(),"Id","UserName"),
                RoleSelectList = new SelectList(_roleService.Query().ToList(), "Id", "Name")
            };

            return View(viewModel);
        }

            
        [HttpPost]
        public IActionResult Index(FilterItemModel filter)
        {
            var model = _reportService.GetList(false, filter);


            return PartialView("_Report", model);
        }





    }
}

