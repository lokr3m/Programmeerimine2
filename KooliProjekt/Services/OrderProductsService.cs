using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class OrderProductsService : IOrderProductsService
    {
        private readonly ApplicationDbContext _context;

        public OrderProductsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<OrderProduct>> List(int page, int pageSize)
        {
            return await _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                .GetPagedAsync(page, 5);
        }

        public async Task<OrderProduct> Get(int id)
        {
            return await _context.OrderProducts.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(OrderProduct list)
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
            var OrderProduct = await _context.OrderProducts.FindAsync(id);
            if (OrderProduct != null)
            {
                _context.OrderProducts.Remove(OrderProduct);
                await _context.SaveChangesAsync();
            }
        }
    }
}
