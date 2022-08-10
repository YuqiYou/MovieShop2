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



        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {

            var favorite1 = await _favoriteRepository.GetFavoriteById(favoriteRequest.UserId, favoriteRequest.MovieId);
            if (favorite1 == null)
            {
                throw new Exception("Favorite not exists");
            }

            var returned = await _favoriteRepository.removeFavorite(favorite1);
            if (returned != null)
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

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = await _purchaseRepository.GetByUserMovie(userId, purchaseRequest.MovieId);
            if (purchase != null)
            {
                throw new Exception("User has already purchased this movie!");
            }
            var dbPurchase = new Purchase
            {
                UserId = userId,
                TotalPrice = (decimal)purchaseRequest.TotalPrice,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId
            };
            var newPurchase = await _purchaseRepository.AddPurchase(dbPurchase);
            if (newPurchase.UserId > 0)
            {
                return true;
            }
            return false;
        }
     public async Task<PurchaseRequestModel> GetPurchaseDetails(int userId, int movieId)
        {
            var purchase = await _purchaseRepository.GetByUserMovie(userId, movieId);

            if (purchase == null) { return null; }
            return new PurchaseRequestModel
            {
                TotalPrice = purchase.TotalPrice,
                MovieId = purchase.MovieId,
                PosterUrl = purchase.Movie.PosterUrl
            };
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var purchase = await _purchaseRepository.GetByUserMovie(userId, purchaseRequest.MovieId);
            if (purchase == null)
            {
                return false;
            }
            return true;
        }

        public async Task<UserModel> GetUserById(int userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new Exception($"Unable to locate user with id={userId}");
            }
            var roles = new List<Role>();
            foreach (var role in user.RolesOfUser)
            {
                roles.Add(role.Role);
            }
            var userModel = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                phoneNumber = user.PhoneNumber,
                profilePictureUrl = user.ProfilePictureUrl,
                roles = roles
            };
            return userModel;
        }
    }
}
