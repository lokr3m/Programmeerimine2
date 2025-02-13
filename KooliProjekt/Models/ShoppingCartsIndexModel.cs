using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Models
{
    [ExcludeFromCodeCoverage]
    public class ShoppingCartsIndexModel
    {
        public ShoppingCartsSearch Search { get; set; }
        public PagedResult<ShoppingCart> Data { get; set; }
    }
}
