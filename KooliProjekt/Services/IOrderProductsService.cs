using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IOrderProductsService
    {
        Task<PagedResult<OrderProduct>> List(int page, int pageSize);
        Task<OrderProduct> Get(int id);
        Task Save(OrderProduct list);
        Task Delete(int id);
    }
}