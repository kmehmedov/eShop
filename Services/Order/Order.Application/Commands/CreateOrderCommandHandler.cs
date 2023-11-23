using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.IntegrationEvents;
using Order.Application.IntegrationEvents.Events;
using Order.Application.Models;
using Order.Domain.Models.Orders;

namespace Order.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CommandResult<OrderDTO>>
    {
        public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger, IOrderIntegrationEventService integrationEventService)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _integrationEventService=integrationEventService;
        }
        public async Task<CommandResult<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Domain.Models.Orders.Order(request.BuyerId);

            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.ProductName, item.UnitQuantity, item.UnitPrice);
            }

            _logger.LogInformation("Creating Order - Order: {@Order}", order);

            await _orderRepository.CreateAsync(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            _integrationEventService.PublishThroughEventBus(new OrderCreatedIntegrationEvent(request.BuyerId, order.Id));

            return new CommandResult<OrderDTO>(order.ToOrderDTO(), CommandResultTypeEnum.Success);
        }

        #region Private members
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private readonly IOrderIntegrationEventService _integrationEventService;
        #endregion
    }
}
