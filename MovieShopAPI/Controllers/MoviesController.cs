using ApplicationCore.Models;
using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }



        [HttpGet]
        [Route("top-revenue")]

        //Attribute Routing
        // MVC http://localhost/movies/GetTopRevenueMovies  =>Traditional/Convention based routng
        // http://localHost/api/movies/top-grossing
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            //call my service
            var movies = await _movieService.GetTopRevenueMovies();
            
            //return the movie info in json format
            //ASP.NET core Automatically serializes c# obj to Json objs
            //system.txt.json (created since .NET 3)
            // older version of .Net to work with JSOn nettonsoft.json
            // return data(json format) alongwith it return the HTTP status code!!

            //null or not anyitems
            if(movies == null || !movies.Any())
            {
                //404
                return NotFound(new {errorMessage = "No Movies Found"});
            }

            return Ok(movies);


        }


        [HttpGet]
        [Route("top-rated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(movies);
        }


        [HttpGet]
        [Route("{Id:int}")]
        public async Task<IActionResult> GetMovie(int Id)
        {
            var movie = await _movieService.GetMovieDetails(Id);

            if (movie == null) {
                return NotFound(new {errorMessage = $"No Movie Found for {Id}"});
            }

            return Ok(movie);

        }


        //[HttpGet]
        //[Route("{genreId:int/pageSize:int/page:int }")]
        //public async Task<IActionResult> GetMovieByPagination(int genreId, int pageSize = 30, int page = 1)
        //{
        //    var movies = await _movieService.GetMoviesByPagination(genreId, pageSize, page);

        //    if (movies == null) { return NotFound(new { errorMessage = "No Movies Found" }); }

        //    return Ok(movies);

        //}


        /// <summary>
        ///     Get collection of movies by pagination and search term for movie title
        ///     default page size is 30
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        /// 
        //[HttpGet]
        //[Route("")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorModel))]
        //public async Task<ActionResult<PagedResultSet<MovieCardModel>>> GetAllMovies([FromQuery] int pageSize = 30,
        //    [FromQuery] int page = 1,
        //    string title = "")
        //{
        //    var movies = await _movieService.GetMoviesByPagination(title, pageSize, page);
        //    if (movies?.Data?.Any() == false)
        //    {
        //        return NotFound(new ErrorModel { Message = "No movies found for search query" });
        //    }

        //    return Ok(movies);
        //}

    }
}
