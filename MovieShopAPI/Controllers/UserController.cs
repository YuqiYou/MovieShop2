using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       // private readonly ICurrentUser _currentUser;
        public UserController(IUserService userService)
        {
            _userService = userService;
         //   _currentUser = currentUser;
        }

        //working
        [HttpGet]
        [Route("details/{id:int}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _userService.GetUserById(id);
            return Ok(user);
        }

        //should be working
        [HttpGet]
        [Route("Purchases/{id:int}")]
        public async Task<IActionResult> GetPurchasesByUser(int id)
        {
            var purchases = await _userService.GetAllPurchasesForUser(id);
            return Ok(purchases);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> PurchaseMovie([FromBody] PurchaseRequestModel purchase, int userId)
        {
            if (await _userService.PurchaseMovie(purchase, userId))
            {
                return Ok(purchase);
            }
            return Conflict(new { errorMessage = "User has already purchased movie" });

        }


        //working
        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteMovie([FromBody] FavoriteRequestModel favorite)
        {
            if (await _userService.AddFavorite(favorite))
            {
                return Ok(favorite);
            }
            return Conflict(new { errorMessage = "Already favorited" });

        }

        [HttpPost]
        [Route("un-favorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] FavoriteRequestModel favorite)
        {
            if (await _userService.RemoveFavorite(favorite))
            {
                return Ok(favorite);
            }
            return Conflict(new { errorMessage = "It is not yet favorited" });
        }

    }
}
