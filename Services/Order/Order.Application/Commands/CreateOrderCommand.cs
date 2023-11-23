using MediatR;
using Order.Application.Models;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<CommandResult<OrderDTO>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }

    }
}
