using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Data
{
    [ExcludeFromCodeCoverage]
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public IdentityUser User { get; set; }
        public string UserId {  get; set; }

        public IList<OrderProduct> OrderProducts { get; set; }
        public string Title { get; set; }

        public Order()
        {
            OrderProducts = new List<OrderProduct>();
        }
    }
}
