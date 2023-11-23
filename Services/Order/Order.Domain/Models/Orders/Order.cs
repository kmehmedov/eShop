using Order.Domain.Exceptions;

namespace Order.Domain.Models.Orders
{
    public class Order : Entity<int>
    {
        public Order(string buyerId)
        {
            BuyerId = buyerId;
            Date = DateTime.UtcNow;
            Status = OrderStatus.Draft;
        }
        public Order()
        {

        }

        public DateTime Date { get; set; }
        public string BuyerId { get; set; }
        public OrderStatus Status { get; private set; }
        public List<OrderItem> OrderItems { get;set; } = new List<OrderItem>();

        public void AddOrderItem(int productId, string productName, int quantity, decimal price)
        {
            var existingOrderForProduct = OrderItems.Where(o => o.ProductId == productId).SingleOrDefault();

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.AddQuantity(quantity);
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, quantity, price);
                OrderItems.Add(orderItem);
            }
        }

        public void Confirm()
        {
            if (Status != OrderStatus.Draft)
            {
                throw new OrderDomainException("Can not confirm order.");
            }
            Status = OrderStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Shipped)
            {
                throw new OrderDomainException("Can not cancel order. It is already shipped.");
            }
            Status = OrderStatus.Cancelled;
        }

        public void Ship()
        {
            if(Status != OrderStatus.Confirmed)
            {
                throw new OrderDomainException("Can not ship order.");
            }
            Status = OrderStatus.Shipped;
        }
    }
}
