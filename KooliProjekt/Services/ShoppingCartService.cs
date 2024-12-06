using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ShoppingCart>> List(int page, int pageSize, ShoppingCartsSearch search)
        {
            return await _context.ShoppingCarts.GetPagedAsync(page, 5);
        }

        public async Task<ShoppingCart> Get(int id)
        {
            return await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(ShoppingCart list)
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
            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart != null)
            {
                _context.ShoppingCarts.Remove(shoppingCart);
                await _context.SaveChangesAsync();
            }
        }
    }
}
