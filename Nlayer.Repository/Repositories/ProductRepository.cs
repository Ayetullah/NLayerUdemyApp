using Microsoft.EntityFrameworkCore;
using Nlayer.Repository.DataContext;
using NLayer.Core.Dtos;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace Nlayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
