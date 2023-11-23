using Order.Application.Models;
using Order.Domain.Models.Orders;

namespace Order.Application
{
    public static class Extensions
    {
        public static IEnumerable<OrderItemDTO> ToOrderItemsDTO(this IEnumerable<OrderItem> orderItems)
        {
            foreach (var item in orderItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }

        public static OrderItemDTO ToOrderItemDTO(this OrderItem orderItem)
        {
            return new OrderItemDTO
            {
                Id = orderItem.Id,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.ProductName,
                UnitPrice = orderItem.UnitPrice,
                UnitQuantity = orderItem.UnitQuantity,
                OrderId = orderItem.OrderId
    };
        }

        public static IEnumerable<OrderDTO> ToOrdersDTO(this IEnumerable<Order.Domain.Models.Orders.Order> items)
        {
            foreach (var item in items)
            {
                yield return item.ToOrderDTO();
            }
        }

        public static OrderDTO ToOrderDTO(this Order.Domain.Models.Orders.Order item)
        {
            return new OrderDTO()
            {
                Id= item.Id,
                Date = item.Date,
                BuyerId = item.BuyerId,
                Status = item.Status.ToString(),
                OrderItems = item.OrderItems.ToOrderItemsDTO().ToList()
            };
        }
    }
}
