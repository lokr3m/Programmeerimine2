using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Models
{
    [ExcludeFromCodeCoverage]
    public class OrderProductsIndexModel
    {
        public OrderProductsSearch Search { get; set; }
        public PagedResult<OrderProduct> Data { get; set; }
    }
}
