namespace Order.Application.Models
{
    public record OrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string BuyerId { get; set; }
        public string Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
