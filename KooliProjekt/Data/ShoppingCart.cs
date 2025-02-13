using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Data
{
    [ExcludeFromCodeCoverage]
    public class ShoppingCart
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalQuantity { get; set; }

        public IList<CartProduct> CartProducts { get; set; }
        public string Title { get; set; }

        public ShoppingCart()
        {
            CartProducts= new List<CartProduct>();
        }
    }
}
