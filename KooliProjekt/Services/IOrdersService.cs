using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IOrdersService
    {
        Task<PagedResult<Order>> List(int page, int pageSize, OrdersSearch search);
        Task<Order> Get(int id);
        Task Save(Order list);
        Task Delete(int id);
    }
}
