using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class CartProductsIndexModel
    {
        public CartProductsSearch Search { get; set; }
        public PagedResult<CartProduct> Data { get; set; }
    }
}
