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
    public class GenreRepository : IGenreRepository

    {
        private readonly MovieShopDbContext _movieshopDbContext;

        public GenreRepository(MovieShopDbContext movieshopDbContext)
        {
            _movieshopDbContext = movieshopDbContext;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var genres = await _movieshopDbContext.Genres.ToListAsync();
            return genres;
        }
    }
}