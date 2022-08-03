using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public  async Task<CastModel> GetCastDetails(int id)
        {
            var Moviecasts = await _castRepository.GetById(id);

            if(Moviecasts == null) { return null; }

            var castModel = new CastModel
            {

                Id = Moviecasts.MovieId,
                Name = Moviecasts.Movie.Title,
                ProfilePath = Moviecasts.Cast.ProfilePath,
                Character = Moviecasts.Character

            };


            return castModel;

         
        }
    }
}
