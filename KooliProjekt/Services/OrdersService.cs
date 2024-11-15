using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;

        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Order>> List(int page, int pageSize)
        {
            return await _context.Orders.GetPagedAsync(page, 5);
        }

        public async Task<Order> Get(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Order list)
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
            var Order = await _context.Orders.FindAsync(id);
            if (Order != null)
            {
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
