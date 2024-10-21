using System.ComponentModel.DataAnnotations;

namespace OnlineShop.API.Models.DTO
{
    public class AddCategoryDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
        public string Name { get; set; }
        [Required]
        [MinLength(2, ErrorMessage = "Description must be at least 2 characters long")]
        public string Description { get; set; }
    }
}
