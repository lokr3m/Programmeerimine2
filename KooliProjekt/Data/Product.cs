using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public int Stock { get; set; }
        public bool IsDone { get; set; }

        public IList<Product> Products { get; set; }
        public string Title { get; set; }

        public Product()
        {
            Products = new List<Product>();
        }
    }
}
