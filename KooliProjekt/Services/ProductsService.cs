using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ProductsService : IProductsService
    {
        private readonly ApplicationDbContext _context;

        public ProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> List(int page, int pageSize)
        {
            return await _context.Products.GetPagedAsync(page, 5);
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Product list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var Product = await _context.Products.FindAsync(id);
            if (Product != null)
            {
                _context.Products.Remove(Product);
                await _context.SaveChangesAsync();
            }
        }
    }
}