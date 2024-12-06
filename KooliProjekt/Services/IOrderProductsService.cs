using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IOrderProductsService
    {
        Task<PagedResult<OrderProduct>> List(int page, int pageSize, OrderProductsSearch query = null);
        Task<OrderProduct> Get(int id);
        Task Save(OrderProduct list);
        Task Delete(int id);
    }
}