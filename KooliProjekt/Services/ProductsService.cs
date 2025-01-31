using KooliProjekt.Data;
using KooliProjekt.Search;
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

        public async Task<PagedResult<Product>> List(int page, int pageSize, ProductsSearch search = null)
        {
            var query = _context.Products.AsQueryable();

            search = search ?? new ProductsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Name.Contains(search.Keyword));
            }

            if (search.Done != null)
            {
                query = query.Where(list => list.Products.Any());

                if (search.Done.Value)
                {
                    query = query.Where(list => list.Products.All(item => item.IsDone));
                }
                else
                {
                    query = query.Where(list => list.Products.Any(item => !item.IsDone));
                }
            }
            return await query
                .OrderBy(list => list.Name)
                .GetPagedAsync(page, pageSize);

        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Save(Product list)
        {
            if (list.Id == 0)
            {
                _context.Products.Add(list);
            }
            else
            {
                _context.Products.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}