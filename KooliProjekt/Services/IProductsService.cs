using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IProductsService
    {
        Task<PagedResult<Product>> List(int page, int pageSize);
        Task<Product> Get(int id);
        Task Save(Product list);
        Task Delete(int id);
    }
}