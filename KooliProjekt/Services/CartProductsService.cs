using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KooliProjekt.Services
{
    public class CartProductsService : ICartProductsService
    {
        private readonly ApplicationDbContext _context;

        public CartProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CartProduct>> List(int page, int pageSize, CartProductsSearch search = null)
        {
            return await _context.CartProducts
                    .Include(op => op.Product)
                    .Include(op => op.ShoppingCart)
                    .GetPagedAsync(page, 5);

            search = search ?? new CartProductsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.list.Name.Contains(search.Keyword));
            }

            if (search.MinQuantity != null)
            {
                query = query.Where(cp => cp.Quantity >= search.MinQuantity.Value);
            }

            if (search.MaxQuantity.HasValue)
            {
                query = query.Where(cp => cp.Quantity <= search.MaxQuantity.Value);
            }

            // Пагинация
            var totalItems = await query.CountAsync();
            var results = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<CartProduct>
            {
                Results = results,
                PageIndex = page,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                TotalItems = totalItems
            };
        }
    }

        public async Task<CartProduct> Get(int id)
        {
            return await _context.CartProducts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(CartProduct list)
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
            var cartProduct = await _context.CartProducts.FindAsync(id);
            if (cartProduct != null)
            {
                _context.CartProducts.Remove(cartProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
