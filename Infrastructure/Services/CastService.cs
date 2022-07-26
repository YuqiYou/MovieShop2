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
            //var casts = await _castRepository.GetById(id);
            //var castModel = casts.Select(g => new CastModel { Id = g.Id, Name = g.Name }).ToList();
            //return castsModel;

            throw new NotImplementedException();
        }
    }
}
