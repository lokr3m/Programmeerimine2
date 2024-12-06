using KooliProjekt.Data;
using KooliProjekt.Search;
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

        public async Task<PagedResult<Order>> List(int page, int pageSize, OrdersSearch search = null)
        {
            var query = _context.Orders.AsQueryable();

            search = search ?? new OrdersSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.Name.Contains(search.Keyword));
            }

            //if (search.Done != null)
            //{
            //    query = query.Where(list => list.Name.Any());

            //    if (search.Done.Value)
            //    {
            //        query = query.Where(list => list.Name.All(item => item.IsDone));
            //    }
            //    else
            //    {
            //        query = query.Where(list => list.Name.Any(item => !item.IsDone));
            //    }
            //}

            return await query.GetPagedAsync(page, pageSize);
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
