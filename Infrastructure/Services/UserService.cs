using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryContracts;
using ApplicationCore.ServiceContracts;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class UserService : IUserService

    {
        private readonly IUserRepository _userRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPurchaseRepository _purchaseRepository;

        public UserService(IUserRepository userRepository, IFavoriteRepository favoriteRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
            _purchaseRepository = purchaseRepository;
        }



        //FAVORITE
        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest)
        {


            var favorite = await _favoriteRepository.GetFavoriteById(favoriteRequest.UserId, favoriteRequest.MovieId);
            if (favorite != null)
            {
                throw new Exception("Favorite exists");
            }

            var newFavorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            };
            var returned = await _favoriteRepository.AddFavorite(newFavorite);
            if (returned.MovieId > 0)
            {
                return true;
            }
            return false;
        }


       
        public async Task<bool> FavoriteExists(int id, int movieId)
        {
            var favorite = await _favoriteRepository.GetFavoriteById(id,movieId);
            if(favorite != null) {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<FavoriteRequestModel>> GetAllFavoritesForUser(int id)
        {

            var FavoriteList = await _favoriteRepository.getAllFavorites(id);

            var FavoriteListModel = new List<FavoriteRequestModel>();

            foreach(var item in FavoriteList)
            {
                FavoriteListModel.Add(new FavoriteRequestModel
                {
                    MovieId = item.MovieId,
                    UserId = item.UserId,
                    MovieTitle = item.Movie.Title,
                    PosterUrl = item.Movie.PosterUrl,


                });    
                    
                    

            }

            return FavoriteListModel;


         

        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            await _favoriteRepository.removeFavorite(favoriteRequest.MovieId, favoriteRequest.UserId);
        }


        //REVIEW
        public Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }



        //Purchase 
        public async Task<List<PurchaseRequestModel>> GetAllPurchasesForUser(int id)
        {
            var purchaseList = await _purchaseRepository.getAllPurchases(id);

            var purchaseListModel = new List<PurchaseRequestModel>();

            foreach (var item in purchaseList)
            {
                purchaseListModel.Add(new PurchaseRequestModel
                {
                    MovieId = item.MovieId,
                    UserId = item.UserId,
                    MovieTitle = item.Movie.Title,
                    PosterUrl = item.Movie.PosterUrl,

                });



            }

            return purchaseListModel;
        }

     
        public Task GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

     
 
    }
}
