using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceContracts
{
    public interface IUserService
    {

        public Task<UserModel> GetUserById(int userId);

        public Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        public Task<List<PurchaseRequestModel>> GetAllPurchasesForUser(int id);
        public Task<PurchaseRequestModel> GetPurchaseDetails(int userId, int movieId);



        Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<bool> FavoriteExists(int id, int movieId);
        Task <List<FavoriteRequestModel>> GetAllFavoritesForUser(int id);
        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        Task GetAllReviewsByUser(int id);
       
    }
}
