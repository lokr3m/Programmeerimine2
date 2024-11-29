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
        public bool IsDone { get; set; }

        public IList<Product> Products { get; set; }

        public Product()
        {
            Products = new List<Product>();
        }
    }
}
