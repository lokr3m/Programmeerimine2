using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.BlazorApp
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Title { get; set; }
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
        [Required]
        [StringLength(10)]
        public string Description { get; set; }
    }
}
    