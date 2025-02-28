using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.Data
{
    [ExcludeFromCodeCoverage]
    public class CartProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public ShoppingCart ShoppingCart { get; set; }
        public int ShoppingCartId { get; set; }
        public string Title { get; set; }
    }
}
