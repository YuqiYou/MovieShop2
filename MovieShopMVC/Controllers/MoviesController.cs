using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController: Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //go to movie service- movie repo and get movie details from movie table

            //Server -> IIS -Windows 
            var movieDetails = await _movieService.GetMovieDetail(id);
            return View(movieDetails);
        }
    }
}
