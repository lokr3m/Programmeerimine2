using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Models
{
    [ExcludeFromCodeCoverage]
    public class ProductsIndexModel
    {
        public ProductsSearch Search { get; set; }
        public PagedResult<Product> Data { get; set; }
    }
}
