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

        public UserService(IUserRepository userRepository, IFavoriteRepository favoriteRepository)
        {
            _userRepository = userRepository;
            _favoriteRepository = favoriteRepository;
        }



        //FAVORITE
        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            

            var favorite = new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
  
            };

            _favoriteRepository.AddFavorite(favorite);
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

            foreach (var Favorite in FavoriteList)
            {

                FavoriteListModel.Add(new FavoriteRequestModel {MovieId = Favorite.MovieId, UserId = Favorite.UserId });
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
        public Task GetAllPurchasesForUser(int id)
        {
            throw new NotImplementedException();
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
