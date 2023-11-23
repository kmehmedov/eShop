using MediatR;
using Order.Application.Models;

namespace Order.Application.Commands
{
    public class CreateOrderFromShoppingCartCommandHandler : IRequestHandler<CreateOrderFromShoppingCartCommand, CommandResult<OrderDTO>>
    {
        public Task<CommandResult<OrderDTO>> Handle(CreateOrderFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Models.Orders.Order(request.BuyerId);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitQuantity, item.UnitPrice);
            }

            return Task.FromResult(new CommandResult<OrderDTO>(order.ToOrderDTO(), CommandResultTypeEnum.Success));
        }
    }
}
