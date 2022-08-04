﻿using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiceContracts;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int movieId)
        {
            var movieDetails = await _movieRepository.GetById(movieId);

            if(movieDetails == null)
            {
                return null;
            }

            var movieDetailsModel = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Title = movieDetails.Title,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                OriginalLanguage = movieDetails.OriginalLanguage,
                Overview = movieDetails.Overview,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Revenue = movieDetails.Revenue,
                ImdbUrl = movieDetails.ImdbUrl,
                TmdbUrl = movieDetails.TmdbUrl,
                RunTime = movieDetails.RunTime,
                Tagline = movieDetails.Tagline,
                Price = movieDetails.Price
            };

            foreach (var trailer in movieDetails.Trailers)
            {
                movieDetailsModel.trailers.Add(new TrailerModel
                {
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                });
            }

            foreach (var cast in movieDetails.CastsOfMovie)
            {
                movieDetailsModel.casts.Add(new CastModel { Id = cast.CastId, Name = cast.Cast.Name, Character = cast.Character, ProfilePath = cast.Cast.ProfilePath });
            }

            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movieDetailsModel.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            return movieDetailsModel;
        }

        public async Task<PagedResultSet<MovieCardModel>> GetMoviesByPagination(int genreId, int pageSize = 30, int page = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenrePagination(genreId, pageSize, page);

            var movieCards = new List<MovieCardModel>();
            movieCards.AddRange(movies.Data.Select(m => new MovieCardModel
            {
                Id = m.Id,
                PosterUrl = m.PosterUrl,
                Title = m.Title
            }));

            return new PagedResultSet<MovieCardModel>(movieCards, page, pageSize, movies.ToalRowCount);
        }

        public async Task<PagedResultSet<MovieCardModel>> GetMoviesByTitlePagination(string title, int pageSize = 30, int page = 1)
        {
            return null;
        }


        public async Task<List<MovieCardModel>> GetTopRevenueMovies()
        {
            // communicate with Repositories

            var movies = await _movieRepository.GetTop30HighestRevenueMovies();

            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }
            return movieCards;
        }

        public async Task<List<MovieCardModel>> GetTopRatedMovies()
        {
            var movies = await _movieRepository.GetTop30RatedMovies();
            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            }
            return movieCards;
        }



        //public async Task<SearchPageModel<MovieCardModel>> GetMoviesBySearch(string title, int page = 1, int pageSize = 30)
        //{
        //    var movies = await _movieRepository.GetByTitle(title, page, pageSize);
        //    var movieCards = new List<MovieCardModel>();
        //    foreach (var movie in movies.Data)
        //    {
        //        movieCards.Add(new MovieCardModel { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
        //    }
        //    return new SearchPageModel<MovieCardModel>(movies.Name, movieCards, page, pageSize, movies.TotalRowCount);
        //}

        //public async Task<ReviewPageModel<ReviewModel>> GetReviewsByMovie(int movieId, int page = 1, int pageSize = 30)
        //{
        //    var reviews = await _movieRepository.GetMovieReviews(movieId, page, pageSize);
        //    if (reviews == null) { return null; }
        //    var ReviewModels = new List<ReviewModel>();
        //    foreach (var review in reviews.Data)
        //    {
        //        ReviewModels.Add(new ReviewModel
        //        {
        //            userId = review.UserId,
        //            movieId = movieId,
        //            title = reviews.Name,
        //            rating = review.Rating,
        //            reviewText = review.ReviewText
        //        });
        //    }
        //    return new ReviewPageModel<ReviewModel>(ReviewModels.First().title, ReviewModels, page, pageSize, reviews.TotalRowCount);
        //}





    }
}