using KooliProjekt.Data;
using KooliProjekt.Search;

namespace KooliProjekt.Models
{
    public class OrdersIndexModel
    {
        public OrdersSearch Search { get; set; }
        public PagedResult<Order> Data { get; set; }
    }
}
