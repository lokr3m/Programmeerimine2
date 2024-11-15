using KooliProjekt.Data;

namespace KooliProjekt.Services
{
    public interface IShoppingCartService
    {
        Task<PagedResult<ShoppingCart>> List(int page, int pageSize);
        Task<ShoppingCart> Get(int id);
        Task Save(ShoppingCart list);
        Task Delete(int id);
    }
}
