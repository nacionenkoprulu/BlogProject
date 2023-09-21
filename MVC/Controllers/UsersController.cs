using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUserService _userService;
        private readonly ICityService _cityService;

        public UsersController(IUserService userService, ICityService cityService)
        {
            _userService = userService;
            _cityService = cityService;
        }

        public IActionResult GetUsersByRole(int roleId)
        {

            var users = _userService.GetListByRole(roleId);


            return Json(users);
        }

        public IActionResult GetCities(int? countryId)
        {
            if(!countryId.HasValue)
            {
                return NotFound();
            }

            List<CityModel> cities = _cityService.GetList(countryId.Value);
            return Json(cities);


        }



    }
}
