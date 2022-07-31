using ApplicationCore.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class CastController: Controller
    {

        private readonly ICastService _castService;

        public CastController(ICastService castService)
        {
            _castService = castService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            //go to movie service- movie repo and get movie details from movie table

            ////Server -> IIS -Windows 
            var castDetails = await _castService.GetCastDetails(id);
            return View(castDetails);


        }

      
    }
}
