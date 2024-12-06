using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class ShoppingCartsIndexModel
    {
        public ShoppingCartsSearch Search { get; set; }
        public PagedResult<ShoppingCart> Data { get; set; }
    }
}
