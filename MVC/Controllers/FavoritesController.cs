using Business.Services;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MVC.Controllers
{
    public class FavoritesController : Controller
    {

        const string SESSIONKEY = "favorites";

        int _userId;

        private readonly IBlogService _blogservice;

        public FavoritesController(IBlogService blogservice)
        {
            _blogservice = blogservice;
        }

        public IActionResult GetFavorites(int blogId)
        {

            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var favoritesList = GetSession(_userId);

            return View("Favorites", favoritesList);
        }

        public IActionResult AddToFavorites(int blogId) 
        {

            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            var blog = _blogservice.Query().SingleOrDefault(b => b.Id == blogId);

            var favoritesList = GetSession(_userId);

            if (favoritesList.Any(f=>f.BlogId == blogId && f.UserId == _userId))
            {
                TempData["Message"] = $"{blog.Title} already added to favorites!";
            }
            else
            {
                var favoritesItem = new FavoriteModel(blogId, _userId, blog.Title, blog.Score ?? 0);

                favoritesList.Add(favoritesItem);

                SetSession(favoritesList);

                TempData["Message"] = $"'{blog.Title}' added to favorites!";
            }

            return RedirectToAction("Index", "Blogs");
        }

        private List<FavoriteModel> GetSession(int userId)
        {


            var favoritesList = new List<FavoriteModel>();

            var favoritesJson = HttpContext.Session.GetString(SESSIONKEY);

            if (!string.IsNullOrWhiteSpace(favoritesJson))
            {
                favoritesList = JsonConvert.DeserializeObject<List<FavoriteModel>>(favoritesJson); //JSON'u C# list objesine dönüştürüyor.

                favoritesList.Where(f => f.UserId == userId).ToList();
            }

            return favoritesList;

        }


        private void SetSession(List<FavoriteModel> favoritesList) //Listeyi sessiona ayarlayan metod. 
        {

            var favoritesJSon = JsonConvert.SerializeObject(favoritesList); //C# Listesini Json'a çevirir

            HttpContext.Session.SetString(SESSIONKEY, favoritesJSon);


        }


        public IActionResult RemoveFromFavorites(int blogId, int userId) //UserID ve BlogID'ye göre Favorites Session'undan elemanları silen action metoduç
        {
            var favoritesList = GetSession(_userId);

            favoritesList.RemoveAll(f=>f.BlogId==blogId && f.UserId == userId); //List'te bu metodu kullanabiliyoruz. DBSet<>'te bu metodu kullanamıyoruz.


            SetSession(favoritesList);

            return RedirectToAction(nameof(GetFavorites));

        }

        public IActionResult ClearFavorites() //UserID'ye ait Favorites Session'unu tamamen temizler.
        {
            var favoritesList = GetSession(_userId);

            _userId = Convert.ToInt32(User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value);

            favoritesList.RemoveAll(f => f.UserId == _userId);

            SetSession(favoritesList);

            return RedirectToAction(nameof(GetFavorites));
        }

    }
}
