using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository:ICastRepository
    {

        private readonly MovieShopDbContext _movieshopDbContext;


        public CastRepository(MovieShopDbContext movieshopDbContext)
        {
            _movieshopDbContext = movieshopDbContext;
        }

 

        public async Task<Movie> GetById(int id)
        {
            var movieDetails = await _movieshopDbContext.Movies
                           .Include(m => m.GenresOfMovie)
                           .ThenInclude(m => m.Genre)
                           .Include(m => m.CastsOfMovie)
                           .ThenInclude(m => m.Cast)
                           .Include(m => m.Trailers)
                           .FirstOrDefaultAsync(m => m.Id == id);
            return movieDetails;
        }
    }
}
