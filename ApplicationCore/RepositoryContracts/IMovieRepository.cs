using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetTop30HighestRevenueMovies();
        Task<List<Movie>> GetTop30RatedMovies();
        Task<Movie> GetById(int id);
        Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1);
        Task<SearchPageModel<Movie>> GetByTitle(string title, int page = 1, int pageSize = 30);

        Task<GenrePageModel<Movie>> GetByGenre(int genreId, int page = 1, int pageSize = 30);
        Task<ReviewPageModel<Review>> GetMovieReviews(int movieId, int page = 1, int pageSize = 1);


    }
}