using KooliProjekt.Data;
using KooliProjekt.Search;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Models
{
    [ExcludeFromCodeCoverage]
    public class CategoriesIndexModel
    {
        public CategoriesSearch Search { get; set; }
        public PagedResult<Category> Data { get; set; }
    }
}
