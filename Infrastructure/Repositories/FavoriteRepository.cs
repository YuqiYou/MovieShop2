using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository:IFavoriteRepository
    {

        private readonly MovieShopDbContext _movieshopDbContext;

        public FavoriteRepository(MovieShopDbContext movieshopDbContext)
        {
            _movieshopDbContext = movieshopDbContext;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
             _movieshopDbContext.Favorites.Add(favorite);
            await _movieshopDbContext.SaveChangesAsync();

            return favorite;
        }
        public async Task<Favorite> removeFavorite(Favorite favorite)
        {


            _movieshopDbContext.Favorites.Remove(favorite);
            await _movieshopDbContext.SaveChangesAsync();

            return favorite;
        }


        //PASSED
        public async Task<List<Favorite>> getAllFavorites(int id)
        {
            var Favorites = await _movieshopDbContext.Favorites
                .Include(f => f.User)
                .Include(f =>f.Movie)
                .Where(f => f.UserId == id).ToListAsync(); 

            return Favorites;
        }

        public async Task<Favorite> GetFavoriteById(int id, int movieId)
        {
            var favorite = await _movieshopDbContext.Favorites
              .FirstOrDefaultAsync(m => m.UserId == id && m.MovieId == movieId);
            return favorite;
        }

  
    }
}
