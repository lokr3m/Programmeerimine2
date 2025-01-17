using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public  string Description { get; set; }

        public IList<Product> Products { get; set; }
        public string Title { get; set; }

        public Category()
        {
            {
                Products = new List<Product>();
            }
        }
    }
}
