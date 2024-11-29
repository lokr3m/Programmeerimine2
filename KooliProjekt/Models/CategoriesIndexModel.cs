using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class CategoriesIndexModel
    {
        public ProductsSearch Search { get; set; }
        public PagedResult<Product> Data { get; set; }
    }
}
