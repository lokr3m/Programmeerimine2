using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IOrdersService
    {
        Task<PagedResult<Order>> List(int page, int pageSize);
        Task<Order> Get(int id);
        Task Save(Order list);
        Task Delete(int id);
    }
}
