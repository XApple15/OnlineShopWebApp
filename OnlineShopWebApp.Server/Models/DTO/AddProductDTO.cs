using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Models.DTO
{
    public class AddProductDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Description must be at least 2 characters long")]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public string? ProductImageURL { get; set; }
    }
}
