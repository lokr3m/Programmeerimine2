using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface IProductsService
    {
        Task<PagedResult<Product>> List(int page, int pageSize, ProductsSearch search);
        Task<Product> Get(int id);
        Task Save(Product list);
        Task Delete(int id);
    }
}