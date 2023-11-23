using MediatR;
using Order.Application.Models;

namespace Order.Application.Commands
{
    public class CreateOrderFromShoppingCartCommand : IRequest<CommandResult<OrderDTO>>
    {
        public string BuyerId { get; set; }
        public List<ShoppingCartItemDTO> OrderItems { get; set; }

    }
}
