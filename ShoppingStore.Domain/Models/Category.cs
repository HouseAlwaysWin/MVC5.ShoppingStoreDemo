using System.ComponentModel.DataAnnotations;

namespace ShoppingStore.Domain.Models
{
    public class Category
    {
        [Required]
        public string Name { get; set; }
    }
}