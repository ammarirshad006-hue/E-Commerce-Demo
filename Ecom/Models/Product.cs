using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Ecom.Models
{
    public class Product
    {
       
        public int Id { get; set; }

      
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string? Name { get; set; }

      
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

      
        [Required(ErrorMessage = "Category is required.")]
        public string? Category { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

      
        [Required(ErrorMessage = "SKU is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "SKU must be a positive number.")]
        public int SKU { get; set; }

      
        [JsonIgnore]
        public virtual ICollection<Stock>? Stock { get; set; }
    }
}

