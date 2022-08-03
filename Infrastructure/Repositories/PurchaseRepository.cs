using ApplicationCore.Entities;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {

        private readonly MovieShopDbContext _movieshopDbContext;

        public PurchaseRepository(MovieShopDbContext movieshopDbContext)
        {
            _movieshopDbContext = movieshopDbContext;
        }
        public async Task<List<Purchase>> getAllPurchases(int id)
        {
            var Purchase = await _movieshopDbContext.Purchases
               .Include(f => f.Movie)
               .Where(f => f.UserId == id).ToListAsync();

            return Purchase;
        }
    }
}
