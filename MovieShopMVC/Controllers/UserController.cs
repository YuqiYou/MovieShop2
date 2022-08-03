using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        //private readonly IHttpContextAccessor _contextAccessor;
        //private readonly ILogger<UserController> _logger;


        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // get all the movies purchased by user , user id
            // httpcontext.user.claims and then call the database and get the inforamtion to the view

            var userId = _currentUser.UserId;

            var PurchaseListModel = await _userService.GetAllPurchasesForUser(userId);

            var MovieCardList = new List<MovieCardModel>();

            foreach (var purchase in PurchaseListModel)
            {

                MovieCardList.Add(new MovieCardModel
                {
                    Id = purchase.MovieId,
                  //  Title = purchase.MovieTitle,
                  //  PosterUrl = Favorite.PosterUrl

                });
            }

            return View(MovieCardList);

        }

        [HttpGet]

        public async Task<IActionResult> Favorites()
        {

            var userId = _currentUser.UserId;

            var favoriteListModel = await  _userService.GetAllFavoritesForUser(userId);

            var MovieCardList = new List<MovieCardModel>();

            foreach (var Favorite in favoriteListModel)
            {

                MovieCardList.Add(new MovieCardModel
                {
                    Id = Favorite.MovieId,
                    Title = Favorite.MovieTitle,
                    PosterUrl = Favorite.PosterUrl

                });
            }

            return View(MovieCardList);
        }


        [HttpPost]
        public async Task<IActionResult> FavoriteMovie(int movieId)
        {
         if (await _userService.FavoriteExists(_currentUser.UserId, movieId)) {
                throw new Exception("Favorite already exists!");
            }


            var favoriteReq = new FavoriteRequestModel{
                MovieId = movieId,
                UserId = _currentUser.UserId
            };

            await _userService.AddFavorite(favoriteReq);
            return LocalRedirect("~/Movies/Details/" + movieId);
        }



        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

    }
}