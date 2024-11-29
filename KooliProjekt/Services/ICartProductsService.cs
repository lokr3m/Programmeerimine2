using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Services
{
    public interface ICartProductsService
    {
        Task<PagedResult<CartProduct>> List(int page, int pageSize, CartProductsSearch search = null);
        Task<CartProduct> Get(int id);
        Task Save(CartProduct list);
        Task Delete(int id);
    }
}
