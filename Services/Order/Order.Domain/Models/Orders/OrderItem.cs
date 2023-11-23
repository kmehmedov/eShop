using Order.Domain.Exceptions;

namespace Order.Domain.Models.Orders
{
    public class OrderItem : Entity<int>
    {

        public OrderItem(int productId, string productName, int quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            UnitQuantity = quantity;
            UnitPrice = price;
        }

        public OrderItem()
        {
        }

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int UnitQuantity { get; private set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public void AddQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new OrderDomainException("Invalid quantity");
            }
            UnitQuantity += quantity;
        }
    }
}
