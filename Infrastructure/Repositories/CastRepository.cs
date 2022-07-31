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

 

        public async Task<MovieCast> GetById(int id)
        {
            var castDetails = await _movieshopDbContext.MovieCasts
                .Include(m=>m.Movie)
                .Include(m=>m.Cast)
                .FirstOrDefaultAsync(m => m.CastId == id);
            return castDetails;
        }
    }
}
