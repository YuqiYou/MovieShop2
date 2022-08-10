using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly MovieShopDbContext _movieShopDbContext;

    public MovieRepository(MovieShopDbContext dbContext)
    {
        _movieShopDbContext = dbContext;
    }

    public async Task<Movie> GetById(int id)
    {
        // select * from movie where id = 1 join genre, cast, moviegerne, moviecast
        var movieDetails = await _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            .Include(m => m.CastsOfMovie).ThenInclude(m => m.Cast)
            .Include(m => m.Trailers)
            .FirstOrDefaultAsync(m => m.Id == id);

       // var rating = await _movieShopDbContext.

        return movieDetails;
    }

    public async Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1)
    {
        // get total row count
        var totalMoviesCountOfGenre = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();
        if (totalMoviesCountOfGenre == 0)
        {
            throw new Exception("No Movies found for this genre");
        }

        // get the actual data
        var movies = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(g => g.Movie).OrderByDescending(m => m.Movie.Revenue)
            .Select(m => new Movie
            {
                Id = m.MovieId,
                PosterUrl = m.Movie.PosterUrl,
                Title = m.Movie.Title
            })
            .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        var pagedMovies = new PagedResultSet<Movie>(movies, page, pageSize, totalMoviesCountOfGenre);
        return pagedMovies;
    }
    public async Task<List<Movie>> GetTop30HighestRevenueMovies()
    {
        // call the database with EF Core and get the data
        // use MovieShopDbContext and Movies DbSet
        // select top 30 * from Movies order by Revenue
        // corresponding LINQ Query

        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Revenue)
            .Select(m => new Movie { Id = m.Id, Title = m.Title, PosterUrl = m.PosterUrl })
            .Take(30).ToListAsync();
        return movies;
    }


    public async Task<List<Movie>> GetTop30RatedMovies()
    {
        var movies = await _movieShopDbContext.Movies.OrderByDescending(m => m.Rating)
             .Take(30)
             .ToListAsync();
        return movies;
    }

    public async Task<SearchPageModel<Movie>> GetByTitle(string title, int page = 1, int pageSize = 30)
    {
        var pattern = @"\b.*" + title + @".*\b";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        /*var hits = await _movieShopDbContext.Movies.Where(m => regex.IsMatch(m.Title)).ToListAsync();*/
        var hits = await _movieShopDbContext.Movies.Where(m => m.Title.Contains(title)).CountAsync();
        if (hits == 0)
        {
            throw new Exception("No movies found matching that title");
        }

        /*var movies = await _movieShopDbContext.Movies.Where(m => regex.IsMatch(m.Title)).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();*/
        var movies = await _movieShopDbContext.Movies.Where(m => m.Title.Contains(title)).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return new SearchPageModel<Movie>(title, movies, page, pageSize, hits);
    }


    public async Task<GenrePageModel<Movie>> GetByGenre(int genreId, int page = 1, int pageSize = 30)
    {
        var genreCount = await _movieShopDbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();
        var genreName = await _movieShopDbContext.Genres.Where(g => g.Id == genreId).Select(g => g.Name).FirstOrDefaultAsync();
        if (genreCount == 0)
        {
            throw new Exception("No movies found in that genre!");
        }

        var movies = await _movieShopDbContext.Movies
            .Include(m => m.GenresOfMovie)
            .Where(m => m.GenresOfMovie.Any(g => g.GenreId == genreId)).Skip((page - 1) * pageSize).Take(pageSize)
            .ToListAsync();

        return new GenrePageModel<Movie>(genreName, movies, page, pageSize, genreCount);
    }

    public async Task<ReviewPageModel<Review>> GetMovieReviews(int movieId, int page = 1, int pageSize = 1)
    {
        var reviews = await _movieShopDbContext.Reviews.Include(r => r.Movie)
        .Where(r => r.MovieId == movieId)
        .OrderByDescending(r => r.CreatedDate)
        .Skip((page - 1) * pageSize).Take(pageSize)
        .ToListAsync();
        return new ReviewPageModel<Review>(reviews.First().Movie.Title, reviews, page, pageSize, reviews.Count());
    }





}