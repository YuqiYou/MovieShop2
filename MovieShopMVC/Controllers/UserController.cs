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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<UserController> _logger;


        public UserController(ICurrentUser currentUser, IUserService userService, IHttpContextAccessor contextAccessor, ILogger<UserController> logger)
        {
            _currentUser = currentUser;
            _userService = userService;
            _contextAccessor = contextAccessor;
            _logger = logger;

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
                    Title = purchase.MovieTitle,
                    PosterUrl = purchase.PosterUrl

                });
            }

            return View(MovieCardList);

        }


        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseRequestModel model, int userId)
        {
            ICurrentUser currentUser = new CurrentUser(_contextAccessor);
            if (currentUser.IsAuthenticated == false)
            {
                return LocalRedirect("~/Account/Login");
            }
            await _userService.PurchaseMovie(model, userId);
            return LocalRedirect("~/Movies/Details/" + model.MovieId);
        }


        [HttpGet]
        public async Task<IActionResult> GetPurchaseDetails(int userId, int movieId)
        {
            var details = await _userService.GetPurchaseDetails(userId, movieId);
            return PartialView("_PurchaseDetails", details);
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
        public async Task<IActionResult> FavoriteMovie(int id)
        {
         if (await _userService.FavoriteExists(_currentUser.UserId, id)) {
                throw new Exception("Favorite already exists!");
            }


            var favoriteReq = new FavoriteRequestModel{
                MovieId = id,
                UserId = _currentUser.UserId
            };

            await _userService.AddFavorite(favoriteReq);
            return LocalRedirect("~/Movies/Details/" + id);
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int id)
        {
            if (!await _userService.FavoriteExists(_currentUser.UserId, id))
            {
                throw new Exception("Favorite never exists!");
            }


            var favoriteReq = new FavoriteRequestModel
            {
                MovieId = id,
                UserId = _currentUser.UserId
            };

            await _userService.RemoveFavorite(favoriteReq);
            return LocalRedirect("~/Movies/Details/" + id);
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