namespace KooliProjekt.Data
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }

        public IList<OrderProduct> OrderProducts { get; set; }

        public Product()
        {
            OrderProducts = new List<OrderProduct>();
        }
    }
}
