using OnlineShop.API.Models.Domain;

namespace OnlineShop.API.Models.DTO
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public CategoryDTO Category { get; set; }
        public string? ProductImageURL { get; set; }
    }
}
