using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Models
{
    [ExcludeFromCodeCoverage]
    public class CartProductsIndexModel
    {
        public CartProductsSearch Search { get; set; }
        public PagedResult<CartProduct> Data { get; set; }
    }
}
