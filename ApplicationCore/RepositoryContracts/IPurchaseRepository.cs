using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryContracts
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> getAllPurchases(int id);

        Task<Purchase> AddPurchase(Purchase purchase);
        Task<Purchase> GetByUserMovie(int userId, int movieId);

    }
}
