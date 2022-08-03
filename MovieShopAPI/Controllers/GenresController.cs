using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }


        [HttpGet]
        [Route("Genres")]

        public async Task<IActionResult> getGenres()
        {
            var genres = await _genreService.GetAllGenres();
            if (genres == null || !genres.Any())
            {

                //404
                return NotFound(new { errorMessage = "No Genres Found" });

            }

            return Ok(genres);

        }

    }
}
