using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface ICartProductsService
    {
        Task<PagedResult<CartProduct>> List(int page, int pageSize);
        Task<CartProduct> Get(int id);
        Task Save(CartProduct list);
        Task Delete(int id);
    }
}