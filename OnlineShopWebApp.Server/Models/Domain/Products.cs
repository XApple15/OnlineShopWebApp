namespace OnlineShop.API.Models.Domain
{
    public class Products
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Guid CategoryId { get; set; }
        public string? ProductImageURL { get; set; }

        public Category Category { get; set; }

    }
}
