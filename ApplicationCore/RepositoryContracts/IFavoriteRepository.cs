using ApplicationCore.Entities;

namespace Infrastructure.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> AddFavorite(Favorite favorite);
        //Task<bool> FavoriteExists(int id, int movieId);

        Task<Favorite> GetFavoriteById(int id, int movieId);

        Task<List<Favorite>> getAllFavorites(int id);
        Task <Favorite> removeFavorite(Favorite favorite);
    }
}