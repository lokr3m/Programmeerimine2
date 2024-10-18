namespace KooliProjekt.Data
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public IList<Product> Products { get; set; }

        public Category()
        {
            {
                Products = new List<Product>();
            }
        }
    }
}
