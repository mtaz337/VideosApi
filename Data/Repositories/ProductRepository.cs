using Microsoft.EntityFrameworkCore;
using VideoApi.Data.Entities;

namespace VideoApi.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyProjectContext _context;

        public ProductRepository(MyProjectContext context)
        {
            _context = context;
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Add other repository methods as needed
    }
}