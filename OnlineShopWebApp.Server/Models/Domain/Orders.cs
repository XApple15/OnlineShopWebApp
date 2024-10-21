namespace OnlineShop.API.Models.Domain
{
    public class Orders
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }
        public double Total { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }

        public Customer Customer { get; set; }
        public Products Product { get; set; }

    }
}
