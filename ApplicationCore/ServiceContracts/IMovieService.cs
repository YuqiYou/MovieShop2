using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceContracts
{
    public interface IMovieService
    {
        Task<List<MovieCardModel>> GetTopRevenueMovies();
        Task<List<MovieCardModel>> GetTopRatedMovies();

        Task<MovieDetailsModel> GetMovieDetails(int movieId);

        Task<PagedResultSet<MovieCardModel>> GetMoviesByPagination(int genreId, int pageSize = 30, int page = 1);

        Task<PagedResultSet<MovieCardModel>> GetMoviesByTitlePagination(string title, int pageSize = 30, int page = 1);

        Task<SearchPageModel<MovieCardModel>> GetMoviesBySearch(string title, int page = 1, int pageSize = 30);
        Task<ReviewPageModel<ReviewModel>> GetReviewsByMovie(int movieId, int page = 1, int pageSize = 30);
        Task<GenrePageModel<MovieCardModel>> GetMoviesByGenre(int genreId, int page = 1, int pageSize = 30);
    }
}